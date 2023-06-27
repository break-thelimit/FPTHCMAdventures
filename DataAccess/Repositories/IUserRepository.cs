using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User UpdateUser(User user);
        IEnumerable<User> GetUserBySchool(String schoolname);
    }
}
