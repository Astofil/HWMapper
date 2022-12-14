using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers;

[ApiController]
[Route("controller")]

public class StudentController
{
    private readonly StudentService _studentService;
    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }


    [HttpGet("Get Student")]
    public async Task<Response<List<GetStudentDto>>> GetStudent()
    {
        return await _studentService.GetStudent();
    }

    [HttpPost("Add Student")]
    public async Task<Response<GetStudentDto>> AddStudent([FromForm]AddStudentDto student)
    {
        return await _studentService.AddStudent(student);
    }

    [HttpPut("Update Student")]
    public async Task<Response<GetStudentDto>> UpdateStudent([FromForm]AddStudentDto student)
    {
        return await _studentService.UpdateStudent(student);
    }

    [HttpDelete("Delete Student")]
    public async Task<Response<string>> DeleteStudent(int id)
    {
        return await _studentService.DeleteStudent(id);
    }
}