using Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class AuthError
{
    public static Error InvalidRegisterRequest => new(ErrorTypeConstant.ValidationError, "Invalid register request");
    public static Error UserAlreadyExist => new(ErrorTypeConstant.ValidationError, "User already exist");
}
