using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Implemetations
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
            _passwordHasher = new PasswordHasher<User>();   
        }

        public UserInfoVM GetUserById(int id)
        {
            if(id == 0)
            {
                throw new NullReferenceException("Id can not be zero");
            }

            User userById = _userRepository.GetUser(id);


            return new UserInfoVM()
            {
                UserName = userById.UserName,
                Email = userById.Email,
                FirstName = userById.FirstName,
                LastName = userById.LastName,
                Address = userById.Address,
                City = userById.City,
                Password = userById.Password,
                PhoneNumber = userById.PhoneNumber,
            };
        }

        public UserViewModel LogInUser(LogInUserViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentException("Provided model is null");
            }

            if(string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentException("Provided data is not valid");
            }

            User user = _userRepository.GetUserByEmail(model.Email);


           if(user is null)
            {
                throw new ArgumentException("No user with that email or password");
            }

           PasswordVerificationResult verificationResult =  
                _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);


            if(verificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Login credentials do not match!");
            }

            return new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FirstName + " " + user.LastName,
                UserName = user.UserName,
                UserRoleKey = user.Role.Key,
            };

        }
       
        public void RegisterUser(RegisterUserViewModel model)
        {
            if (model == null) throw new ArgumentNullException("Provided model is null.");
        
            if (model.Password != model.ConformationPassword)
                throw new ArgumentException("Provided passwords are not equal.");

            User user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = "-",
                PhoneNumber = "-",
                City = "-",
                RoleId = 2,
                UserName = model.UserName,  
               
            };

            string hashPasword = _passwordHasher.HashPassword(user, model.Password);
            user.Password = hashPasword;

            _userRepository.Insert(user);   
        }
    }
}
