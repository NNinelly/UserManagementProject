using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Services.JwtPasswordService;
using UserManagementProject.Context;

namespace UserManagement.Application.User.UserService
{
    public class UserServiceFactory : IUser
    {
        private readonly UserManagementDbContext ent = new UserManagementDbContext();
        private readonly IConfiguration _configuration;
        private IJwtPasswordService _jwtPasswordService;
        public UserServiceFactory(UserManagementDbContext entity, IConfiguration configuration, 
            IJwtPasswordService jwtPasswordService)
        {
            ent = entity;
            _configuration = configuration;
            _jwtPasswordService = jwtPasswordService;
        }

        public async Task<bool> AddUser(UserServiceModel request)
        {
            if (ent.Users.Any(i => i.Email == request.Email || i.UserName == request.UserName))
            {
                throw new Exception("This User Already Exist");
            }
            _jwtPasswordService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new Domain.Entities.User()
            {
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email,
                IsActive = request.IsActive,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            
            await ent.Users.AddAsync(user);
            await ent.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUser(UserServiceModel request)
        {
            var user = await ent.Users.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
            user.UserName = request.UserName;
            user.Password = request.Password;
            user.Email = request.Email;
            user.IsActive = request.IsActive;

            ent.Users.Update(user);
            await ent.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await ent.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            user.Id = id;

            ent.Users.Remove(user);
            await ent.SaveChangesAsync();
            return true;
        }
        public async Task<List<UserServiceModel>> GetUsers()
        {
            var res = await ent.Users.Select(i => new UserServiceModel
            {
                Id = i.Id,
                UserName = i.UserName,
                Password = i.Password,
                Email = i.Email,
                IsActive = i.IsActive
            }).ToListAsync();

            return res;
        }

        public string Login(string email, string password)
        {
            if (!ent.Users.Any(i => i.Email == email))
            {
                throw new Exception("The User does not exist");
            }
            var user = ent.Users.Where(x => x.Email == email).FirstOrDefault();
            var passwordhash = user.PasswordHash;

            var passwordSalt = user.PasswordSalt;
            if (!_jwtPasswordService.VerifyPasswordHash(password, passwordhash, passwordSalt))
            {
                throw new Exception("Wrong Password");
            }

            string token = _jwtPasswordService.GenerateToken(user);
            return token;
        }
    }
}
