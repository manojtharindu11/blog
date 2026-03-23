using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Result
{
    public class ErrorTypeConstant
    {
        public const string None = "None";
        public const string ValidationError = "ValidationError";
        public const string UnAuthorized = "UnAuthorizedError";
        public const string NotFound = "NotFoundError";
        public const string InternalServerError = "InternalServerError";
        public const string Forbidden = "ForbiddenError";
    }
}
