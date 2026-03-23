using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Result
{
    public sealed record Error(string Code, string Message)
    {
        internal static Error None => new(ErrorTypeConstant.None, string.Empty);
    }
}
