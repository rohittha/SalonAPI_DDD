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
    public class AuthCommandService : IAuthCommandService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //1. Check if user exists
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
                //throw new Exception("User already exists!");
            }

            //2. Create user(generate unique id)
            var user = new User { FirstName = firstName, LastName = lastName, Email = email, Password = password };
            _userRepository.Add(user);

            //3. Create token
            //Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }


    }
}
