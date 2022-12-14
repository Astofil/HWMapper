namespace Domain.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    // public int DepartmentId { get; set; }

    public virtual List<Enrollment> Enrollments { get; set; }
}