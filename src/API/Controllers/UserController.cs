using API.Extensions;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController(IUserService userService) : BaseAPIController
    {
        [HttpGet]

        public async Task<IResult> GetAllUsers
        (
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var users = await userService.GetAsync(pageNumber, pageSize);
            return users.ToHttpResponse();
        }
    }
}
