using Application.Common.Result;
using Application.DTOs;
using Application.Error;
using Application.Interface;
using Application.Models.Request;
using Application.Validators;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        UserUpdateRequestValidator userUpdateRequestValidator,
        IUnitOfWork unitOfWork) : IUserService
    {
        public Task<Result<string>> AssignRoleAsync(AssignRoleRequest roleRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);

                if (user is null)
                {
                    return Result.Failure<string>(UserError.UserNotFound);
                }

                userRepository.Delete(user);
                await unitOfWork.CommitAsync();
                return Result.Success("User deleted successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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

        public async Task<Result<UserDto>> GetByIdAsync(int id)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);
                if (user is null)
                {
                    return Result.Failure<UserDto>(UserError.UserNotFound);
                }

                var userDetails = new UserDto(
                                   user.Id,
                                   user.Email,
                                   user.UserName,
                                   user.UserRoles.Select(x => x.Role.Name).ToList());

                return Result.Success(userDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Result<string>> UpdateAsync(UserUpdateRequest userUpdateRequest)
        {
            try
            {
                var validationResult = await userUpdateRequestValidator.ValidateAsync(userUpdateRequest);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(a => a.ErrorMessage);
                    return Result.Failure<string>(UserError.CreateInvalidUserUpdateRequestError(errors));
                }

                var user = await userRepository.GetByIdAsync(userUpdateRequest.Id);

                if (user is null)
                {
                    return Result.Failure<string>(UserError.UserNotFound);
                }

                user.UserName = userUpdateRequest.UserName;
                user.Email = userUpdateRequest.Email;
                userRepository.Update(user);
                await unitOfWork.CommitAsync();
                return Result.Success("User updated successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
