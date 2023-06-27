using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private UserDAO() { }

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    users = context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetAllUsers: " + ex.Message);
            }
            return users;
        }

        public User UpdateUser(User user)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                context.Users.Update(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error at UpdateUser: " + ex.Message);
            }
            return user;
        }

        public IEnumerable<User> GetUserBySchool(String schoolname)
        {
            List<User> users = new List<User>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    users = context.Users.Where(p=>p.School.SchoolName.Equals(schoolname)).ToList();
                }
            }catch(Exception ex)
            {
                throw new Exception("Error at GetUserBySchool: " + ex.Message);
            }
            return users;
        }
    }
}
