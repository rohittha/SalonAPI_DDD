using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Salon.Application.Authentication.Commands.Register;
using Salon.Application.Authentication.Common;
using Salon.Application.Authentication.Queries.Login;
using Salon.Contracts.Authentication;

namespace Salon.API.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command =  new RegisterCommand(request.FirstName,request.LastName, request.Email, request.Password);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query =  new LoginQuery(request.Email, request.Password);
            var authResult = await _mediator.Send(query);

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        public static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token
                );
        }
    }

}
