using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<List<string>> GetUserRoleByEmailAsync(string email);
    }
}
