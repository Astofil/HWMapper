using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;

namespace Infrastructure.Services;

public class StudentService
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IMapper _mapper;

    public StudentService(DataContext context,IWebHostEnvironment environment,IMapper mapper)
    {
        _context = context;
        _environment = environment;
        _mapper = mapper;
    }
    public async Task<Response<List<GetStudentDto>>> GetStudent()
    {
        var list = await _context.Students.Select(t => new GetStudentDto()
        {
            StudentId = t.StudentId,
            FirstName = t.FirstName,
            LastName = t.LastName,
            FileName = t.FileName
        }).ToListAsync();
        await _context.SaveChangesAsync();
        return new Response<List<GetStudentDto>>(list);
    }

    public async Task<Response<GetStudentDto>> AddStudent(AddStudentDto student)
    {

        // var newStudent = new Student()
        // {
        //     StudentId = student.StudentId,
        //     FirstName = student.FirstName,
        //     LastName = student.LastName,
        //     EnrollmentDate = student.EnrollmentDate,
        //     FileName = student.File.FileName
        // };

        var newStudent = _mapper.Map<Student>(student); 

        // var response = new GetStudentDto()
        // {
        //     StudentId = student.StudentId,
        //     FirstName = student.FirstName,
        //     LastName = student.LastName,
        //     EnrollmentDate = student.EnrollmentDate,
        //     FileName = student.File.FileName
        // };

            var response = _mapper.Map<GetStudentDto>(student);
        

        newStudent.FileName = await UploadFile(student.File);
        _context.Students.Add(newStudent);
        await _context.SaveChangesAsync();
        return new Response<GetStudentDto>(response);

        // _context.Students.Add(newStudent);
        // await _context.SaveChangesAsync();
        // return new Response<AddStudentDto>(response);
    }

    public async Task<Response<GetStudentDto>> UpdateStudent(AddStudentDto student)
    {

        // var response = new GetStudentDto()
        // {
        //     StudentId = student.StudentId,
        //     FirstName = student.FirstName,
        //     LastName = student.LastName,
        //     FileName = student.FirstName
        // };

        var response = _mapper.Map<GetStudentDto>(student);

        var find = await _context.Students.FindAsync(student.StudentId);
        find.StudentId = student.StudentId;
        find.FirstName = student.FirstName;
        find.LastName = student.LastName;
        find.FileName = student.FirstName;

        if(student.File != null)
        {
            find.FileName = await UpdateFile(student.File, find.FileName);
        }

        await _context.SaveChangesAsync();
        return new Response<GetStudentDto>(response);
    }

    public async Task<Response<string>> DeleteStudent(int id)
    {
        var find = await _context.Students.FindAsync(id);
        _context.Students.Remove(find);
        await _context.SaveChangesAsync();
        return new Response<string>("Student succesfully deleted");
    }


//--------------Uploading File----------------------------------------------
    private async Task<string> UploadFile(IFormFile file)
    {
        if(file == null) return null;


        var path = Path.Combine(_environment.WebRootPath, "studentimage");
        if(Directory.Exists(path) == false) Directory.CreateDirectory(path);

        var filepath = Path.Combine(path, file.FileName);
        using (var stream = new FileStream( filepath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        
        return file.FileName;
    }

//------------------Updating File-------------------------------------------------
    private async Task<string> UpdateFile(IFormFile file, string oldFileName)
    {
        //delete old file if exists
        var filepath = Path.Combine(_environment.WebRootPath, "studentimage", oldFileName);
        if(File.Exists(filepath) == true) File.Delete(filepath);

        var newFilepath = Path.Combine(_environment.WebRootPath, "studentimage", file.FileName);
        using (var stream = new FileStream(newFilepath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return file.FileName;

    }
}
// dotnet add package Microsoft.AspNetCore.OpenApi --version 7.0.1