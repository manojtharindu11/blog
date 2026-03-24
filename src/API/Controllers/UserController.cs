using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseAPIController
    {
        [Authorize]
        [HttpGet]

        public string[] GetUsers()
        {
            return new[]
            {
                "User 1", "User 2", "User 3", "User 4", "User 5"
            };
        }
    }
}
