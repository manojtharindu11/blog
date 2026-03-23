using Application.Interface;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController(IAuthenticationService authenticationService) : BaseAPIController
    {
        [HttpPost("register")]
        public async Task<IResult> Register(RegisterRequest registerRequest)
        {
            var response = await authenticationService.RegisterAsync(registerRequest);
            if (response.IsFailure)
            {
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequest loginRequest)
        {
            return Results.Ok();
        }
    }
}
