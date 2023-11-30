using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Common.Result;
using UserManagement.Application.Interfaces;
using UserManagement.Application.User.UserProfileService;
using UserManagementProject.Context;

namespace UserManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserProfileController : ControllerBase
    {
        private readonly UserManagementDbContext _userManagementDbContext;
        private readonly IUserProfile _userProfile;

        public UserProfileController(UserManagementDbContext userManagementDbContext, IUserProfile userProfile)
        {
            _userManagementDbContext = userManagementDbContext;
            _userProfile = userProfile;
        }

        [HttpPost("AddUserProfile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<GetResponse> AddUserProfile(UserProfileServiceModel request)
        {
            try
            {
                var res = await _userProfile.AddUserProfile(request);
                return new GetResponse { StatusCode = 200, Message = "You have successfully add you information", Result = res };
            }
            catch (Exception ex)
            {
                return new GetResponse { Message = ex.Message, StatusCode = 400 };
            }
        }

        [HttpPut("UpdateUserProfile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<GetResponse> UpdateUserProfile(UserProfileServiceModel request)
        {
            try
            {
                var res = await _userProfile.UpdateUserProfile(request);
                return new GetResponse { StatusCode = 200, Message = "The User Profile updated successfully", Result = res };
            }
            catch (Exception ex)
            {
                return new GetResponse { Message = ex.Message, StatusCode = 400 };
            }
        }


        [HttpDelete("DeleteUserProfile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<GetResponse> DeleteUserProfile(int id)
        {
            try
            {
                var res = await _userProfile.DeleteUserProfile(id);
                return new GetResponse { StatusCode = 200, Message = "The user profile has been deleted", Result = res };
            }
            catch (Exception ex)
            {
                return new GetResponse { Message = ex.Message, StatusCode = 400 };
            }
        }

        [HttpGet("GetAllUserProfile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<GetUserProfileResponse> GetAllUserProfiles()
        {
            try
            {
                var res = await _userProfile.GetUserProfiles();
                return new GetUserProfileResponse { StatusCode = 200, Message = "Success", Result = res };
            }
            catch (Exception ex)
            {
                return new GetUserProfileResponse { Message = ex.Message, StatusCode = 400 };
            }
        }
    }
}
