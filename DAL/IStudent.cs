using Project1.Models;

namespace Project1.DAL
{
    public interface IStudent
    {
        public IEnumerable<Student> GetAll();
        public Student GetById(int id);
        public IEnumerable<Student> GetByName(string name);
        public IEnumerable<Student> GetAllWithCourse();
        public Student GetStudentWithCourse(int studentId);
        public Student Insert(Student student);
        public Student Update(Student student);
        public void Delete(int id);
    }
}
