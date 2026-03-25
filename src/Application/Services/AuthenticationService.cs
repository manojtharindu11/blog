using Application.Common.Result;
using Application.Interface;
using Application.Models.Request;
using FluentValidation;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthenticationService(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        IValidator<LoginRequest> loginRequestValidator, 
        IValidator<RegisterRequest> registerRequestValidator,
        IJwtService jwtService) : IAuthenticationService
    {     
        public async Task<Result> LoginAsync(LoginRequest loginRequest)
        {
            var validationResult = await loginRequestValidator.ValidateAsync(loginRequest);
             
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(a => a.ErrorMessage);
                return Result.Failure(AuthError.CreateInvalidLoginRequestError(errors));
            }

            var (email, password) = (loginRequest);

            var user = await userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return Result.Failure(AuthError.UserNotFound);
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Result.Failure(AuthError.InvalidPassword);
            }

            var token = await jwtService.GenerateTokenAsync(user);

            var result = new
            {
                Token = token,
                Username = user.UserName
            };

            return Result.Success(result);
        }

        public async Task<Result> RegisterAsync(RegisterRequest registerRequest)
        {
            var validationResult = await registerRequestValidator.ValidateAsync(registerRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(a => a.ErrorMessage);
                return Result.Failure(AuthError.CreateInvalidRegisterRequestError(errors));
            }

            var userExist = await userRepository.GetUserByEmailAsync(registerRequest.Email);

            if (userExist is not null)
            {
                return Result.Failure(AuthError.UserAlreadyExist);
            }

            var user = new User { 
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                UserName = registerRequest.UserName,
                UserRoles = [new UserRole { RoleId = 3 }]
            };

            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(user, registerRequest.Password);
            user.Password = hashedPassword;

            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            return Result.Success("User registered sucessfully!");

        }
    }
}
