using Project1.Models;

namespace Project1.DAL
{
    public interface ICourse
    {
        public IEnumerable<Course> GetAll();
        public Course GetById(int id);
        public IEnumerable<Course> GetByName(string name);
        public IEnumerable<Course> GetAllWithStudent();
        public Course GetCourseWithStudent(int studentId);
        public Course Insert(Course course);
        public Course Update(Course course);
        public void Delete(int id);
    }
}
