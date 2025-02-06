using Application.Interface;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services
{
    public class AuthenticationService(IUnitOfWork unitOfWork, IUserRepository userRepository) : IAuthenticationService
    {     
        public Task<string> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterAsync(RegisterRequest registerRequest)
        {
            if (registerRequest == null) throw new ArgumentNullException(nameof(registerRequest));

            var userExist = await userRepository.GetUserByEmailAsync(registerRequest.Email);

            if (userExist is not null)
            {
                return "User is alread exist";
            }

            var user = new User { 
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                UserName = registerRequest.UserName
            };

            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            return "Register success!";

        }
    }
}
