namespace Project1.DTO
{
    public class CourseWithStudentDto
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public List<EnrollmentWithStudentDto> Enrollments { get; set; }
    }
}
