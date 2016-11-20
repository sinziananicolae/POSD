using POSD_Tema1.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSD_Tema1.Services.UserService
{
    public class UserService
    {
        private readonly Entities _dbEntities;

        public UserService()
        {
            _dbEntities = new Entities();
        }

        public int GetUser(string username, string password) {
            var user = _dbEntities.Users.Where(f => f.Username == username && f.Password == password).FirstOrDefault();

            if (user != null) {
                return user.Id;
            }

            return -1;
        }

        public void CreateUser(string username, string password) {
            var user = new User();

            user.Username = username;
            user.Password = password;

            _dbEntities.Users.Add(user);
            _dbEntities.SaveChanges();
        }
    }
}
