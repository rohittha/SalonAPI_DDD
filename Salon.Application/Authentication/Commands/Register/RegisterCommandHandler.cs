using ErrorOr;
using MediatR;
using Salon.Application.Authentication.Commands.Register;
using Salon.Application.Authentication.Common;
using Salon.Application.Common.Interfaces.Authentication;
using Salon.Application.Services.Persistence;
using Salon.Domain.Common.Errors;
using Salon.Domain.Entities;

namespace Salon.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            //1. Check if user exists
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
                //throw new Exception("User already exists!");
            }

            //2. Create user(generate unique id)
            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };
            _userRepository.Add(user);

            //3. Create token
            //Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
