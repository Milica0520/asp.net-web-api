using Lamazon.Services.Validators;
using System.ComponentModel.DataAnnotations;


namespace Lamazon.Services.ViewModels.User
{
    public class RegisterUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and include a letter, a number, and a special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConformationPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [EmailDomainVlidator(ErrorMessage = "The email domain does not exist.")]

        [EmailExistsValidator]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
