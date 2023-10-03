using ErrorOr;
using Salon.Application.Common.Interfaces.Authentication;
using Salon.Application.Services.Authentication.Common;
using Salon.Application.Services.Persistence;
using Salon.Domain.Common.Errors;
using Salon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Application.Services.Authentication.Commands
{
    public class AuthQueryService : IAuthQueryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // 1. Check if user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
                //throw new Exception("User not found!");     // Return generic exception not specific
            }

            // 2. Validate password
            if (user.Password != password)
            {
                return new[] { Errors.Authentication.InvalidCredentials };
                //throw new Exception("Invalid password!");
            }

            // 3. Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
