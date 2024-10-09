﻿using HairSalon.Core;
using HairSalon.ModelViews.ServiceAppointmentModelViews;

namespace HairSalon.Contract.Services.Interface
{
    public interface IServiceAppointment
    {
        Task<BasePaginatedList<ServiceAppointmentModelView>> GetAllServiceAppointment(int pageNumber, int pageSize, string? id, string? serviceId, string? appointmentId);
        Task<string> DeleteServiceAppointment(string id);
        Task<string> EditServiceAppointment(string id, EditServiceAppointmentModelView editServiceAppointmentModelView);
        Task<string> CreateServiceAppointment(CreatServiceAppointmentModelView creatServiceAppointmentModelView);
    }
}

