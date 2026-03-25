using Domain.Entities;
using Domain.Interface;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRoleRepository(BlogDbContext blogDbContext) : IUserRoleRepository
    {
        public async Task<bool> AddAsync(int userId, int roleId)
        {
            try
            {
                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                };

                await blogDbContext.UserRoles.AddAsync(userRole);
                var result = await blogDbContext.SaveChangesAsync() > 0;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> HasRoleAsync(int userId, int roleId)
        {
            try
            {
                var isUserHasRole = await blogDbContext
                    .UserRoles
                    .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

                return isUserHasRole;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> RemoveAsync(int userId, int roleId)
        {
            try
            {
                var userRole = blogDbContext
                    .UserRoles
                    .FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);

                if (userRole is null)
                {
                    return false;
                }

                blogDbContext.UserRoles.Remove(userRole);
                var result = await blogDbContext.SaveChangesAsync() > 0;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
