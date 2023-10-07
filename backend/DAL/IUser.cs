using backend.DTos;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public interface IUser
    {
        public IEnumerable<User> getAllUsers();
        public User getUserById(int id);
        public void adduser(User user);
        public void deleteUser(int id);
        public bool checkUserWithEmail(string email);
        public loginResponseDto login(string email, string password);

    }
}
