using Application.Common.Result;
using Application.DTOs;
using Application.Interface;
using Application.Models.Request;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public Task<Result<string>> AssignRoleAsync(AssignRoleRequest roleRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<PagedResult<UserDto>>> GetAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var users = await userRepository.GetAllAsync();
                var totalCount = users.Count;
                var pagedItems = users
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(user => new UserDto(
                        user.Id,
                        user.Email,
                        user.UserName,
                        user.UserRoles.Select(x => x.Role.Name).ToList()
                      ))
                    .ToList();

                var pageResult = new PagedResult<UserDto>
                {
                    Items = pagedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return Result.Success(pageResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<Result<UserDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> UpdateAsync(UserUpdateRequest user)
        {
            throw new NotImplementedException();
        }
    }
}
