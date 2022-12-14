using Domain.Entities;

namespace Domain.Dtos;

public class GetCourseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
}