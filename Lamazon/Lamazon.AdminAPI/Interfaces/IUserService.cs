using Lamazon.AdminAPI.DTOs;

namespace Lamazon.AdminAPI.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetUsers();
        UserDto GetUserById(int id);
        UserDto GetUserByEmail(string email);
        void CreateUser(UserDto user);
        void UpdateUser(UserDto user);  
        void DeleteUser(int id);    
    }
}
