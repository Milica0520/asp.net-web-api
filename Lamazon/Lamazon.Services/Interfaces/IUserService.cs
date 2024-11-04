using Lamazon.Services.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserViewModel model);

        UserViewModel LogInUser(LogInUserViewModel model);
    }
}
