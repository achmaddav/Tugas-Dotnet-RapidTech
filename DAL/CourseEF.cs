using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.DAL
{
    public class CourseEF : ICourse
    {
        private AppDbContext _dbcontext;

        public CourseEF(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Delete(int id)
        {
            var deleteCoure = GetById(id);
            if (deleteCoure == null)
                throw new Exception($"Data Course dengan id {id} tidak ditemukan!");
            try
            {
                _dbcontext.Remove(deleteCoure);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Course> GetAll()
        {
            var results = _dbcontext.Courses.OrderBy(c => c.Title).ToList();
            return results;
        }

        public IEnumerable<Course> GetAllWithStudent()
        {
            var results = _dbcontext.Courses.Include(c => c.Enrollments).ThenInclude(c => c.Student)
                .ToList();
            return results;
        }

        public Course GetById(int id)
        {
            var result = _dbcontext.Courses.FirstOrDefault(c => c.CourseID == id);
            if (result == null)
                throw new Exception($"Data id {id} tidak ditemukan");
            return result;
        }

        public IEnumerable<Course> GetByName(string name)
        {
            var result = _dbcontext.Courses.Where(c => c.Title.Contains(name)).OrderBy(c => c.Title);
            return result;
        }

        public Course GetCourseWithStudent(int courseId)
        {
            var result = _dbcontext.Courses.Include(c => c.Enrollments).ThenInclude(c => c.Student)
                .FirstOrDefault(c => c.CourseID == courseId);
            if (result == null)
                throw new Exception($"Course id {courseId} tidak ditemukan");

            return result;
        }

        public Course Insert(Course course)
        {
            try
            {
                _dbcontext.Courses.Add(course);
                _dbcontext.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Course Update(Course course)
        {
            var updateCourse = GetById(course.CourseID);
            if (updateCourse == null)
                throw new Exception($"Data course id {course.CourseID} tidak ditemukan");

            try
            {
                updateCourse.Title = course.Title;
                updateCourse.Credits = course.Credits;
                _dbcontext.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
