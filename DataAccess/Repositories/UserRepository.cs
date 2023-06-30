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
        public bool CreateAccount(string role, User newAccount) => UserDAO.Instance.CreateAccount(role,newAccount);

        public User GetUserById(Guid id) => UserDAO.Instance.GetUserById(id);

        public IEnumerable<User> GetUserBySchool(string schoolname) => UserDAO.Instance.GetUserBySchool(schoolname);

        public IEnumerable<User> GetUsers() => UserDAO.Instance.GetUsers();

        public User UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
    }
}
