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

        public User GetUserById(Guid id)
        {
            User user = new User();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    user = context.Users.SingleOrDefault(a => a.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetUserByID: " + ex.Message);
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

        private User CheckEmailExist(string email)
        {
            User acc = null;
            try
            {
                var db = new FPTHCMAdventuresDBContext();
                acc = db.Users.FirstOrDefault(a => a.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new Exception("Error at CheckEmailExist: " + ex.Message);
            }
            return acc;
        }

        public bool CreateAccount(string role, User newAccount)
        {
            bool check = false;
            if (CheckEmailExist(newAccount.Email) != null)
            {
                return check;
            }
            try
            {
                var db = new FPTHCMAdventuresDBContext();
                User acc = new User
                {
                    Id = Guid.NewGuid(),
                    Email = newAccount.Email,
                    Password = newAccount.Password,
                    RoleId=GetRoleIdByName(role),
                    Status = "VALID",
                };
                if (role.Equals("PLAYER"))
                {

                }
                db.Users.Add(acc);
                check = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error at CreateAccount:" + ex.Message);
            }
            return check;
        }

        public Guid GetRoleIdByName(string rolename)
        {
            Guid id = new Guid();
            try
            {
                var db = new FPTHCMAdventuresDBContext();
                if (rolename.Equals("ADMIN"))
                {
                    id = db.Roles.SingleOrDefault(r => r.RoleName.Equals("Staff")).Id;
                }
                else if (rolename.Equals("PLAYER"))
                {
                    id = db.Roles.SingleOrDefault(r => r.RoleName.Equals("User")).Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetRoleId: " + ex.Message);
            }
            return id;
        }
    }
}
