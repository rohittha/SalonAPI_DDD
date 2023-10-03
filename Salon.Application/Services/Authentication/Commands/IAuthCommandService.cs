using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Salon.Application.Services.Authentication.Common;

namespace Salon.Application.Services.Authentication.Commands
{
    public interface IAuthCommandService
    {
        ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);

    }
}
