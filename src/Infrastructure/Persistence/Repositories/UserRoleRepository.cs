using Domain.Interface;
using Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRoleRepository(BlogDbContext blogDbContext) : IUserRoleRepository
    {
        public Task<bool> AddAsync(int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasRoleAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int userId, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
