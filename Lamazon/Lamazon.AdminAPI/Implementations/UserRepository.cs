using Lamazon.AdminAPI.Domein;
using Lamazon.AdminAPI.Interfaces;

namespace Lamazon.AdminAPI.Implementations
{
    public class UserRepository : IUserRepository
    {

        private readonly AdminDbContext _context;   

        public UserRepository(AdminDbContext context) { 
        _context = context;
        }
        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            List<User> users = _context.Users.ToList(); 
            return users;
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
