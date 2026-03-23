using Application.Common.Result;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IAuthenticationService
    {
        Task<Result> RegisterAsync(RegisterRequest registerRequest);
        Task<Result> LoginAsync(LoginRequest loginRequest);
    }
}
