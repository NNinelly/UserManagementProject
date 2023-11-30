using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Common.Result;

namespace UserManagement.Application.User.UserProfileService
{
    public class UserProfileServiceModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonalNumber { get; set; }

    }

    public class GetUserProfileResponse : ResultClass
    {
        public List<UserProfileServiceModel> Result { get; set; }
    }
}
