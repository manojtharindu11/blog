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
    public class UserRepository(BlogDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<string>> GetUserRoleByEmailAsync(string email)
        {
           return await context.Users
                .Where(x => x.Email == email)
                .SelectMany(x => x.UserRoles)
                .Select(x => x.Role.Name)
                .ToListAsync();
        }
    }
}
