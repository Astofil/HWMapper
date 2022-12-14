using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services;

public class CourseService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CourseService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetCourseDto>>> GetCourse()
    {
        var list = await _context.Courses.Select(t => new GetCourseDto()
        {
            CourseId = t.CourseId,
            Title = t.Title,
            Credits = t.Credits,
        }).ToListAsync();
        await _context.SaveChangesAsync();
        return new Response<List<GetCourseDto>>(list);
    }

    public async Task<Response<AddCourseDto>> AddCourse(AddCourseDto course)
    {
        // var newCourse = new Course()
        // {
        //     CourseId = course.CourseId,
        //     Title = course.Title,
        //     Credits = course.Credits
        // };

        var newCourse = _mapper.Map<Course>(course);

        _context.Courses.Add(newCourse);
        await _context.SaveChangesAsync();
        return new Response<AddCourseDto>(course);
    }

    public async Task<Response<AddCourseDto>> UpdateCourse(AddCourseDto course)
    {
        var find = await _context.Courses.FindAsync(course.CourseId);
        find.CourseId = course.CourseId;
        find.Title = course.Title;
        find.Credits = course.Credits;
        await _context.SaveChangesAsync();
        return new Response<AddCourseDto>(course);
    }
    
    public async Task<Response<string>> DeleteCourse(int id)
    {
        var find = await _context.Courses.FindAsync(id);
        _context.Courses.Remove(find);
        await _context.SaveChangesAsync();
        return new Response<string>("Course succesfully deleted");
    }
}