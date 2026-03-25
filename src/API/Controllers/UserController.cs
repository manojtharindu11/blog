using API.Extensions;
using Application.Interface;
using Application.Models.Request;
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

        [HttpPut]

        public async Task<IResult> UpdateUser([FromBody] UserUpdateRequest userUpdateRequest)
        {
            var result = await userService.UpdateAsync(userUpdateRequest);
            return result.ToHttpResponse();
        }
    }
}
