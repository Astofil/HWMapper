using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class CourseController
{
    private readonly CourseService _courseService;
    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet("Get Course")]
    public async Task<Response<List<GetCourseDto>>> GetCourse()
    {
        return await _courseService.GetCourse();
    }

    [HttpPost("Add Course")]
    public async Task<Response<AddCourseDto>> AddCourse(AddCourseDto course)
    {
        return await _courseService.AddCourse(course);
    }

    [HttpPut("Update Course")]
    public async Task<Response<AddCourseDto>> UpdateCourse(AddCourseDto course)
    {
        return await _courseService.UpdateCourse(course);
    }

    [HttpDelete("Delete Course")]
    public async Task<Response<string>> DeleteCourse(int id)
    {
        return await _courseService.DeleteCourse(id);
    }
}