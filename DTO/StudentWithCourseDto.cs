using Project1.Models;

namespace Project1.DTO
{
    public class StudentWithCourseDto
    {

        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public List<EnrollmentWithCourseDto> Enrollments { get; set; }
    }
}
