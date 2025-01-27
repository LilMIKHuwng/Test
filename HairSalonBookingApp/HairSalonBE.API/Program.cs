﻿using System.Reflection;
using System.Text.Json.Serialization;
using HairSalonBE.API;
using Microsoft.OpenApi.Models;
using HairSalon.ModelViews.Message;
using HairSalon.Services.SignalIR;
using HairSalon.Contract.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserRoomConnection>>(opt =>
    new Dictionary<string, UserRoomConnection>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Hair Salon API", Version = "v1" });
    // Document Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

	option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.WithOrigins("http://localhost:4200")
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials();
	});
});


builder.Services.AddConfig(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
// Enable CORS
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapControllers();

// Run the email task in a background thread
// Run the email and auto-cancel tasks in a background thread
Task.Run(async () =>
{
	using var scope = app.Services.CreateScope();
	var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
	var appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();

	var emailTaskDelay = TimeSpan.FromHours(12); // Gửi email mỗi 12 giờ
	var autoCancelTaskDelay = TimeSpan.FromMinutes(5); // Kiểm tra tự động hủy mỗi 5 phút

	var nextEmailRun = DateTime.UtcNow.Add(emailTaskDelay);
	var nextAutoCancelRun = DateTime.UtcNow.Add(autoCancelTaskDelay);

	while (true)
	{
		try
		{
			// Gửi email nếu đã đến thời gian
			if (DateTime.UtcNow >= nextEmailRun)
			{
				await emailService.SendEmailToConfirmDateAsync();
				nextEmailRun = DateTime.UtcNow.Add(emailTaskDelay);
			}

			// Kiểm tra tự động hủy lịch hẹn nếu đã đến thời gian
			if (DateTime.UtcNow >= nextAutoCancelRun)
			{
				await appointmentService.AutoCheckCancelAppointmentAsync();
				nextAutoCancelRun = DateTime.UtcNow.Add(autoCancelTaskDelay);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error during background tasks: {ex.Message}");
		}

		// Đợi 1 phút trước khi kiểm tra lại (giảm tải CPU, tùy chỉnh nếu cần)
		await Task.Delay(TimeSpan.FromMinutes(1));
	}
});

app.Run();
