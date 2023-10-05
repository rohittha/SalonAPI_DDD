using ErrorOr;
using MediatR;
using Salon.Application.Authentication.Common;
using Salon.Application.Common.Interfaces.Authentication;
using Salon.Application.Services.Persistence;
using Salon.Domain.Common.Errors;
using Salon.Domain.Entities;

namespace Salon.Application.Authentication.Queries.Login
{

    public class LoginQueryhandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryhandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;    // Will Remove this when we add async logic here according to the video
            // 1. Check if user exists
            if (_userRepository.GetUserByEmail(command.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 2. Validate password
            if (user.Password != command.Password)
            {
                return new[] { Errors.Authentication.InvalidCredentials };
            }

            // 3. Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
