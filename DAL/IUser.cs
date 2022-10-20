using Project1.DTO;

namespace Project1.DAL
{
    public interface IUser
    {
        Task Registration(AddUserDto user);
        IEnumerable<UserGetDto> GetAll();
        Task<UserGetDto> Authenticate(AddUserDto user);
    }
}
