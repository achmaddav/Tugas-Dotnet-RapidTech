using Project1.Models;

namespace Project1.DTO
{
    public class GetEnrollmentDto
    {
        public int EnrollmentID { get; set; }
        public int Grade { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
    }
}
