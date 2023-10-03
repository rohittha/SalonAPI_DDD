using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Application.Authentication.Commands.Register.cs
{
    public record RegisterCommand
    (
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}
