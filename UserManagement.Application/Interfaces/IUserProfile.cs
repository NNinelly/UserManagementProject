using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.User.UserProfileService;

namespace UserManagement.Application.Interfaces
{
    public interface IUserProfile
    {
        public Task<bool> AddUserProfile(UserProfileServiceModel request);
        public Task<bool> UpdateUserProfile(UserProfileServiceModel request);
        public Task<bool> DeleteUserProfile(int id);
        public Task<List<UserProfileServiceModel>> GetUserProfiles();
    }
}
