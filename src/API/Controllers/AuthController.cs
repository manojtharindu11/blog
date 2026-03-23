using API.Extensions;
using Application.Common.Result;
using Application.Interface;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers
{
    public class AuthController(IAuthenticationService authenticationService) : BaseAPIController
    {
        [HttpPost("register")]
        public async Task<IResult> Register(RegisterRequest registerRequest)
        {
            var response = await authenticationService.RegisterAsync(registerRequest);
            return response.ToHttpResponse();
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequest loginRequest)
        {
            var response = await authenticationService.LoginAsync(loginRequest);
            return response.ToHttpResponse();
        }
    }
}
