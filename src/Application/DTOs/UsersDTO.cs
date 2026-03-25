using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record UsersDTO(int Id, string Email, string Username, List<string> Roles);
}
