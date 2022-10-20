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
            throw new NotImplementedException();
        }

        public IEnumerable<Enrollment> GetAll()
        {
            var results = _dbcontext.Enrollments.Include(e => e.Student);
            return results;
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
