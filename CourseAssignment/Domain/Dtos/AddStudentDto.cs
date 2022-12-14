using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;

public class AddStudentDto
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public IFormFile File { get; set; }
}