using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.DAL
{
    public class StudentEF : IStudent
    {
        private AppDbContext _dbcontext;

        public StudentEF(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Delete(int id)
        {
            var deleteStudent = GetById(id);
            if (deleteStudent == null)
                throw new Exception($"Data student dengan id {id} tidak ditemukan!");
            try
            {
                _dbcontext.Remove(deleteStudent);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Student> GetAll()
        {
            var results = _dbcontext.Students.OrderBy(s => s.LastName).ToList();
            return results;
        }

        public IEnumerable<Student> GetAllWithCourse()
        {
            var results = _dbcontext.Students.Include(s => s.Enrollments).ThenInclude(s => s.Course)
                .ToList();
            return results;
        }

        public Student GetById(int id)
        {
            var result = _dbcontext.Students.FirstOrDefault(s => s.ID == id);
            if (result == null)
                throw new Exception($"Data id {id} tidak ditemukan");
            return result;
        }

        public IEnumerable<Student> GetByName(string name)
        {
            var result = _dbcontext.Students.Where(s => s.FirstMidName.Contains(name)).OrderBy(s => s.FirstMidName);
            return result;
        }

        public Student GetStudentWithCourse(int studentId)
        {
            var result = _dbcontext.Students.Include(s => s.Enrollments).ThenInclude(s=>s.Course)
                .FirstOrDefault(s => s.ID == studentId);
            if (result == null)
                throw new Exception($"Student id {studentId} tidak ditemukan");

            return result;
        }

        public Student Insert(Student student)
        {
            try
            {
                _dbcontext.Students.Add(student);
                _dbcontext.SaveChanges();
                return student;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Student Update(Student student)
        {
            var updateStudent = GetById(student.ID);
            if (updateStudent == null)
                throw new Exception($"Data student id {student.ID} tidak ditemukan");

            try
            {
                updateStudent.LastName = student.LastName;
                updateStudent.FirstMidName = student.FirstMidName;
                updateStudent.EnrollmentDate = student.EnrollmentDate;
                _dbcontext.SaveChanges();
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
