using Application.Common.Result;
using Application.Interface;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services
{
    public class AuthenticationService(IUnitOfWork unitOfWork, IUserRepository userRepository) : IAuthenticationService
    {     
        public async Task<Result> LoginAsync(LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return Result.Failure(AuthError.InvalidLoginRequest);
            }

            var (email, password) = (loginRequest);

            var user = await userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return Result.Failure(AuthError.UserNotFound);
            }

            if (user.Password != password)
            {
                return Result.Failure(AuthError.InvalidPassword);
            }

            var token = "token"; // will be replaced later

            var result = new
            {
                Token = token,
                Username = user.UserName
            };

            return Result.Success(result);
        }

        public async Task<Result> RegisterAsync(RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return Result.Failure(AuthError.InvalidRegisterRequest);
            }

            var userExist = await userRepository.GetUserByEmailAsync(registerRequest.Email);

            if (userExist is not null)
            {
                return Result.Failure(AuthError.UserAlreadyExist);
            }

            var user = new User { 
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                UserName = registerRequest.UserName
            };

            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            return Result.Success("User registered sucessfully!");

        }
    }
}
