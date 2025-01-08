using Lamazon.AdminAPI.Domein;

namespace Lamazon.AdminAPI.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        void AddUser(User user);    
        User GetUserById(int id);

        void UpdateUser(User user);

        void DeleteUser(int id);    


    }
}
