namespace Domain.Entities;

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string FileName { get; set; }

    public virtual List<Enrollment> Enrollments { get; set; }
}