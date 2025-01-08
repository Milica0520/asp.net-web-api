using Lamazon.AdminAPI.Domein;

namespace Lamazon.AdminAPI.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int RoleId { get; set; }

       

      
    }
}
