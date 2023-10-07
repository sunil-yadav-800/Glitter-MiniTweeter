using backend.DTos;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public class Users:IUser
    {
        private readonly DataBaseContext db;
        public Users(DataBaseContext _db)
        {
            db = _db;
        }
        public IEnumerable<User> getAllUsers()
        {
            return db.users.ToList();
        }
        public User getUserById(int id)
        {
            return db.users.Find(id);
        }
        public void adduser(User user)
        {
            db.users.Add(user);
           var id = db.SaveChanges();
            //return id;
        }
        public void deleteUser(int id)
        {
            var user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
        }
        public bool checkUserWithEmail(string email)
        {
            var user = db.users.Where(u => u.Email == email);
            if (user != null && user.Count()==1)
                return true;
            else
                return false;
        }

        public loginResponseDto login(string email, string password)
        {
            var user = db.users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if(user!=null && user.Email != string.Empty)
            {
                var loggedInUser = new loginResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    firstName = user.firstName,
                    lastName = user.lastName,
                    profilePic = user.profilePic,
                    Contact = user.Contact,
                    Country = user.Country
                };
                return loggedInUser;
            }
            return null;
        }
    }
}
