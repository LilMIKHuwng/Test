﻿using HairSalon.Contract.Repositories.Entity;
using HairSalon.Contract.Services.Interface;
using HairSalon.Core;
using HairSalon.ModelViews.ServiceAppointmentModelViews;
using Microsoft.AspNetCore.Mvc;

namespace HairSalonBE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceAppointmentController : ControllerBase
{
    private readonly IServiceAppointment _serviceAppointment;

    public ServiceAppointmentController(IServiceAppointment serviceAppointment)
    {
        _serviceAppointment = serviceAppointment;
    }

    [HttpGet]
    public async Task<ActionResult<BasePaginatedList<ServiceAppointmentModelView>>> GetAllServicesAppointments()
    {
        try
        {
            var result = await _serviceAppointment.GetAllServiceAppointment();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<ServiceAppointmentModelView>>
        GetAllServicesAppointmentsById(string id)
    {
        try
        {
            var result = await _serviceAppointment.GetServiceAppointmentById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<ServiceAppointment>> CreateServiceAppointment(
        CreatServiceAppointmentModelView creatServiceAppointmentModelView)
    {
        try
        {
            var result = await _serviceAppointment.CreateServiceAppointment(creatServiceAppointmentModelView);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<Boolean>> UpdateServiceAppointment(
         EditServiceAppointmentModelView editServiceAppointmentModelView)
    {
        try
        {
            var result = await _serviceAppointment.EditServiceAppointment(editServiceAppointmentModelView);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
    }

    [HttpGet("get-all-by-service/{serviceID}")]
    public async Task<ActionResult<BasePaginatedList<ServiceAppointmentModelView>>>
        GetAllServiceAppointmentByServiceEntity(string serviceID)
    {
        try
        {
            var result = await _serviceAppointment.GetAllServiceAppointmentByServiceId(serviceID);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
    }


    [HttpGet("/get-by-appointment-id/{appointmentsId}")]
    public async Task<ActionResult<BasePaginatedList<ServiceAppointmentModelView>>>
        GetServiceAppointmentByAppointmentId(string appointmentsId)
    {
        try
        {
            var result = await _serviceAppointment.GetAllServiceAppointmentByAppointmentID(appointmentsId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
    }

    [HttpGet("/get-by-user-id/{userid}")]
    public async Task<ActionResult<BasePaginatedList<ServiceAppointmentModelView>>>
        GetServiceAppointmentByUserId(string userid)
    {
        try
        {
            var result = await _serviceAppointment.GetAllServiceAppointmentByUserId(userid);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<Boolean>> DeleteServiceAppointment(
        DeleteServiceAppointmentModelView deleteServiceAppointmentModelView)
    {
        try
        {
            var result = await _serviceAppointment.DeleteServiceAppointment(deleteServiceAppointmentModelView);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}