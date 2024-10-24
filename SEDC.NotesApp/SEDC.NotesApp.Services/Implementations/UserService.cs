using Lucene.Net.Search;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XAct.Diagnostics.Services.Implementations;
using XSystem.Security.Cryptography;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string LoginUser(LoginUserDto loginDto)
        {
           if(string.IsNullOrEmpty(loginDto.UserName)|| string.IsNullOrEmpty(loginDto.Password))
            {
                throw new UserDataException("Username and password required!");
            }

           MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();


            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginDto.Password);

            byte[] paswordHashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string passwordHash = Encoding.ASCII.GetString(paswordHashBytes);


            User user = _userRepository.LoginUser(loginDto.UserName,passwordHash);


            if(user == null ) {

                throw new UserNotFoundException($"No user with {loginDto.UserName}");
            }

            //generate jwt Token
            JwtSecurityTokenHandler jwtSecurityTokenhandler = new JwtSecurityTokenHandler();


            //
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Academy_academy_1234567890"));


            //
            List<Claim> userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("userFullName", $"{user.FirstName} {user.LastName}"),
                new Claim("isAdmin", "False"),
                new Claim("canAccessAllNotes", "False")
            };

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),

                Subject = new ClaimsIdentity(userClaims),

            };


            SecurityToken token = jwtSecurityTokenhandler.CreateToken(securityTokenDescriptor);


            string userJwtToken = jwtSecurityTokenhandler.WriteToken(token);

          return userJwtToken; 
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {

            //validate data
            ValidateUser(registerUserDto);

            //hash password
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();


            
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            byte[] hash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string passwordHash = Encoding.ASCII.GetString(hash);

            User user = new User()
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Password = passwordHash,
            };

            _userRepository.Add(user);

        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if(string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmedPassword))
            {
                throw new UserDataException("Username and password are required fields!");
            }
            if(registerUserDto.Username.Length > 30)
            {
                throw new UserDataException("Username: Maximum length for username is 30 characters");
            }
            if(!string.IsNullOrEmpty(registerUserDto.FirstName) && registerUserDto.FirstName.Length > 50)
            {
                throw new UserDataException("Maximum length for FirstName is 50 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.LastName) && registerUserDto.LastName.Length > 50)
            {
                throw new UserDataException("Maximum length for LastName is 50 characters");
            }
            if(registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserDataException("Passwords must match!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if(userDb != null)
            {
                throw new UserDataException($"The username {registerUserDto.Username} is already in use!");
            }
        }
    }
}
