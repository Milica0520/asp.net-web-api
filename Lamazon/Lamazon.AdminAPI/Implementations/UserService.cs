using Lamazon.AdminAPI.DTOs;
using Lamazon.AdminAPI.Interfaces;

namespace Lamazon.AdminAPI.Implementations
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository; 


        public UserService(IUserRepository userRepository) { 
        _userRepository = userRepository;
        }
        public void CreateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetUsers()
        {
            List<UserDto> users =  _userRepository.GetAllUsers().Select(user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                RoleId = user.RoleId,
                FirstName = user.FirstName,
                LastName  = user.LastName,
               
            }).ToList();

            return users;
        }

        public void UpdateUser(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
