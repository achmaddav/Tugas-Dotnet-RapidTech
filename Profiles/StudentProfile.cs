using AutoMapper;
using Project1.DTO;
using Project1.Models;

namespace Project1.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, GetStudentDto>();
            CreateMap<AddStudentDto, Student>();
            CreateMap<Student, StudentWithCourseDto>();

            CreateMap<Course, GetCourseDto>();
            CreateMap<AddCourseDto, Course>();
            CreateMap<Course, CourseWithStudentDto>();

            CreateMap<Enrollment, GetEnrollmentDto>();
            CreateMap<Enrollment, EnrollmentWithCourseDto>();
            CreateMap<Enrollment, EnrollmentWithStudentDto>();
            CreateMap<AddEnrollmentStudentToCourseDto, Enrollment>();

        }
    }
}
