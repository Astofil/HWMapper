using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Mapper;

public class ServiceProfile:Profile
{
    public ServiceProfile()
    {
        // CreateMap<AddStudentDto,Student>()
        //     .ForMember(dest=>dest.FileName,opt=>opt.MapFrom(src=>src.File.FileName));
        // CreateMap<Student, GetStudentDto>();
        CreateMap<AddCourseDto,Course>();
        CreateMap<AddEnrollmentDto, Enrollment>();
        CreateMap<AddStudentDto, Student>();
        CreateMap<Student, GetStudentDto>();
        CreateMap<AddStudentDto, GetStudentDto>();
    }
}