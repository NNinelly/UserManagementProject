using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Common.Result;

namespace UserManagement.Application.User.UserService
{
    public class UserServiceModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
    public class GetUserResponse : ResultClass
    {
        public List<UserServiceModel> Result { get; set; }
    }

}
