using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.User.UserService;

namespace UserManagement.Application.Interfaces
{
    public interface IUser
    {
        public Task<bool> AddUser(UserServiceModel request);
        public Task<bool> UpdateUser(UserServiceModel request);
        public Task<bool> DeleteUser(int id);
        public Task<List<UserServiceModel>> GetUsers();
        string Login(string email, string password);
    }
}
