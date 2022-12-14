using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services;

public class EnrollmentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public EnrollmentService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<GetEnrollmentDto>>> GetEnrollment()
    {
        var list = await _context.Enrollments.Select(t => new GetEnrollmentDto()
        {
            EnrollmentId = t.EnrollmentId,
            CourseTitle = t.Course.Title,
            CourseCredits = t.Course.Credits,
            StudentFirstName = t.Student.FirstName,
            StudentLastName = t.Student.LastName,
            StudentEnrollment = t.Student.EnrollmentDate,
            Grade = t.Grade
        }).ToListAsync();
        await _context.SaveChangesAsync();
        return new Response<List<GetEnrollmentDto>>(list);
    }

    public async Task<Response<AddEnrollmentDto>> AddEnrollment(AddEnrollmentDto enrollment)
    {
        // var newEnrollment = new Enrollment()
        // {
        //     EnrollmentId = enrollment.EnrollmentId,
        //     CourseId = enrollment.CourseId,
        //     StudentId = enrollment.StudentId,
        //     Grade = enrollment.Grade
        // };

        var newEnrollment = _mapper.Map<Enrollment>(enrollment);

        _context.Enrollments.Add(newEnrollment);
        await _context.SaveChangesAsync();
        return new Response<AddEnrollmentDto>(enrollment);
    }

    public async Task<Response<AddEnrollmentDto>> UpdateEnrollment(AddEnrollmentDto enrollment)
    {
        var find = await _context.Enrollments.FindAsync(enrollment.EnrollmentId);
        find.EnrollmentId = enrollment.EnrollmentId;
        find.CourseId = enrollment.CourseId;
        find.StudentId = enrollment.StudentId;
        find.Grade = enrollment.Grade;
        await _context.SaveChangesAsync();
        return new Response<AddEnrollmentDto>(enrollment);
    }

    public async Task<Response<string>> DeleteEnrollment(int id)
    {
        var find = await _context.Enrollments.FindAsync(id);
        _context.Enrollments.Remove(find);
        await _context.SaveChangesAsync();
        return new Response<string>("Enrollment succesfully deleted");
    }
}