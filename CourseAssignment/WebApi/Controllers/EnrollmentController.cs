using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("controller")]

public class EnrollmentController
{
    private readonly EnrollmentService _enrollmentService;
    public EnrollmentController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }


    [HttpGet("Get Enrollment")]
    public async Task<Response<List<GetEnrollmentDto>>> GetEnrollment()
    {
        return await _enrollmentService.GetEnrollment();
    }

    [HttpPost("Add Enrollment")]
    public async Task<Response<AddEnrollmentDto>> AddEnrollment(AddEnrollmentDto enrollment)
    {
        return await _enrollmentService.AddEnrollment(enrollment);
    }

    [HttpPut("Update Enrollment")]
    public async Task<Response<AddEnrollmentDto>> UpdateEnrollment(AddEnrollmentDto enrollment)
    {
        return await _enrollmentService.UpdateEnrollment(enrollment);
    }

    [HttpDelete("Delete Enrollment")]
    public async Task<Response<string>> DeleteEnrollment(int id)
    {
        return await _enrollmentService.DeleteEnrollment(id);
    }
}