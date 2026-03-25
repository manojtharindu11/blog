using Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Error
{
    public static class UserError
    {
        public static Common.Result.Error InternalServerError =>
            new(ErrorTypeConstant.InternalServerError, "Something went wrong");
        public static Common.Result.Error UserNotFound =>
            new(ErrorTypeConstant.NotFound, "User not found");
        public static Common.Result.Error CreateInvalidUserUpdateRequestError(IEnumerable<string> errors) =>
            new(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
        public static Common.Result.Error CreateInvalidUserLoginRequestError(IEnumerable<string> errors) =>
            new(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    }
}
