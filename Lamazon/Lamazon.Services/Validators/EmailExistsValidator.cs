using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Implementations;
using Lamazon.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Validators
{
    public class EmailExistsValidator : ValidationAttribute
    {
       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           
            var email = value as string;

            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult("Email is required.");
            }

            var dbContext = (LamazonDbContext)validationContext.GetService(typeof(LamazonDbContext));


            
            bool emailExists = dbContext.Users.Any(u => u.Email == email); 

            if (emailExists)
            {
                return new ValidationResult("The email already exists. Try to register with another email.");
            }

            return ValidationResult.Success;
        }
    }
}
