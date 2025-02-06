using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseAPIController
    {
        [HttpPost("register")]
        public async Task<IResult> Register(RegisterRequest registerRequest)
        {
            return Results.Ok();
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequest loginRequest)
        {
            return Results.Ok();
        }
    }
}
