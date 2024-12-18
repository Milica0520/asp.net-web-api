using Lamazon.DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace Lamazon.Services.Validators
{
    
    public class EmailDomainVlidator : ValidationAttribute
    {

       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
        


            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult("Email is required.");
            }
        

            try
            {
                var host = email.Split('@')[1]; 
                var entry = Dns.GetHostEntry(host);
                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("The email domain does not exist.");
            }
        }
    }
}
