using Lamazon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IUserRepository
    {

        int Insert(User user);
        User GetUser(int id);
        User GetUserByEmail(string email);
    }
}
