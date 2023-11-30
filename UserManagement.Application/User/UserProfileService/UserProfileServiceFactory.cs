using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagementProject.Context;

namespace UserManagement.Application.User.UserProfileService
{
    public class UserProfileServiceFactory : IUserProfile
    {
        private readonly UserManagementDbContext ent = new UserManagementDbContext();
        private readonly IConfiguration _configuration;
        public UserProfileServiceFactory(UserManagementDbContext entity,
            IConfiguration configuration)
        {
            ent = entity;
            _configuration = configuration;
        }

        public async Task<bool> AddUserProfile(UserProfileServiceModel request)
        {
            if (ent.UserProfiles.Any(i => i.PersonalNumber == request.PersonalNumber))
            {
                throw new Exception("This UserProfile Already Exist");
            }

            var userProfile = new UserProfile()
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PersonalNumber = request.PersonalNumber
            };
            
            await ent.UserProfiles.AddAsync(userProfile);
            await ent.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserProfile(UserProfileServiceModel request)
        {
            var userProfile = await ent.UserProfiles.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
            userProfile.UserId = request.UserId;
            userProfile.FirstName = request.FirstName;
            userProfile.LastName = request.LastName;
            userProfile.PersonalNumber = request.PersonalNumber;

            ent.UserProfiles.Update(userProfile);
            await ent.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserProfile(int id)
        {
            var userProfile = await ent.UserProfiles.Where(x => x.Id == id).FirstOrDefaultAsync();

            userProfile.Id = id;

            ent.UserProfiles.Remove(userProfile);
            await ent.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserProfileServiceModel>> GetUserProfiles()
        {
            var res = await ent.UserProfiles.Select(i => new UserProfileServiceModel
            {
                Id = i.Id,
                UserId = i.UserId,
                FirstName = i.FirstName,
                LastName = i.LastName,
                PersonalNumber = i.PersonalNumber
            }).ToListAsync();

            return res;
        }
    }
}
