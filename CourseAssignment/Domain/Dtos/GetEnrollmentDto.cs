using Domain.Entities;

namespace Domain.Dtos;

public class GetEnrollmentDto
{
    public int EnrollmentId { get; set; }
    public string CourseTitle { get; set; }
    public int CourseCredits { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public DateTime StudentEnrollment { get; set; }
    public int Grade { get; set; }
}