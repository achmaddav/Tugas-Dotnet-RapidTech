using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.DAL
{
    public class EnrollmentEF : IEnrollment
    {
        private AppDbContext _dbcontext;

        public EnrollmentEF(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Delete(int id)
        {
            var deleteEnrollment = GetById(id);
            if (deleteEnrollment == null)
                throw new Exception($"Data enrollment dengan id {id} tidak ditemukan!");
            try
            {
                _dbcontext.Remove(deleteEnrollment);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Enrollment> GetAll()
        {
            var results = _dbcontext.Enrollments.Include(e => e.Student);
            return results;
        }

        public Enrollment GetById(int id)
        {
            var result = _dbcontext.Enrollments.FirstOrDefault(e => e.EnrollmentID == id);
            if (result == null)
                throw new Exception($"Data id {id} tidak ditemukan");
            return result;
        }

        public Enrollment Insert(Enrollment enrollment)
        {
            try
            {
                _dbcontext.Enrollments.Add(enrollment);
                _dbcontext.SaveChanges();
                return enrollment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveCourseFromStudent(int studentId)
        {
            try
            {
                _dbcontext.Database.ExecuteSqlInterpolated($"exec dbo.DeleteQuotesForSamurai {studentId}");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
