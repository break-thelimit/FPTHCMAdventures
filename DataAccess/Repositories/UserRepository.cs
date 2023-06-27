using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetUserBySchool(string schoolname) => UserDAO.Instance.GetUserBySchool(schoolname);

        public IEnumerable<User> GetUsers() => UserDAO.Instance.GetUsers();

        public User UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
    }
}
