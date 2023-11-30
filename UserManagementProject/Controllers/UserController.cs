using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UserManagement.Application.Common.Result;
using UserManagement.Application.Interfaces;
using UserManagement.Application.User.UserService;
using UserManagementProject.Context;

namespace UserManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManagementDbContext _userManagementDbContext;
        private readonly IUser _user;

        public UserController(UserManagementDbContext userManagementDbContext, IUser user)
        {
            _userManagementDbContext = userManagementDbContext;
            _user = user;
        }

        [HttpPost("AddUser")]
        public async Task<GetResponse> AddUser(UserServiceModel request)
        {
            try
            {
                var res = await _user.AddUser(request);
                return new GetResponse { StatusCode = 200, Message = "You have successfully registered", Result = res };
            }
            catch (Exception ex)
            {
                return new GetResponse { Message = ex.Message, StatusCode = 400 };
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<GetResponse> UpdateUser(UserServiceModel request)
        {
            try
            {
                var res = await _user.UpdateUser(request);
                return new GetResponse { StatusCode = 200, Message = "The user updated successfully", Result = res };
            }
            catch (Exception ex)
            {
                return new GetResponse { Message = ex.Message, StatusCode = 400 };
            }
        }


        [HttpDelete("DeleteUser")]
        public async Task<GetResponse> DeleteUser(int id)
        {
            try
            {
                var res = await _user.DeleteUser(id);
                return new GetResponse { StatusCode = 200, Message = "The user has been deleted", Result = res };
            }
            catch (Exception ex)
            {
                return new GetResponse { Message = ex.Message, StatusCode = 400 };
            }
        }

        [HttpGet("GetAllUser")]
        public async Task<GetUserResponse> GetAllUser()
        {
            try
            {
                var res = await _user.GetUsers();
                return new GetUserResponse { StatusCode = 200, Message = "Success", Result = res };
            }
            catch (Exception ex)
            {
                return new GetUserResponse { Message = ex.Message, StatusCode = 400 };
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            var res = _user.Login(email, password);
            return Ok(res);
        }
    }
}
