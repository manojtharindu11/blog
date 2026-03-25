using Application.Common.Result;
using Application.Models.Request;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<Result<PagedResult<UserDto>>> GetAsync(int pageNumber = 1, int pageSize = 10);
        Task<Result<string>> UpdateAsync(UserUpdateRequest user);
        Task<Result<string>> DeleteAsync(int id);
        Task<Result<UserDto>> GetByIdAsync(int id);
        Task<Result<string>> AssignRoleAsync(AssignRoleRequest roleRequest);
    }
}
