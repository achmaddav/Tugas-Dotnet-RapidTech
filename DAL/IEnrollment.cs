using Project1.Models;

namespace Project1.DAL
{
    public interface IEnrollment
    {
        public IEnumerable<Enrollment> GetAll();
        public Enrollment Insert (Enrollment enrollment);
        public void Delete(int id);
    }
}
