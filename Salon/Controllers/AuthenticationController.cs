using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Salon.Application.Services.Authentication;
using Salon.Application.Services.Authentication.Commands;
using Salon.Application.Services.Authentication.Common;
using Salon.Contracts.Authentication;

namespace Salon.API.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {        
        private readonly IAuthCommandService _authCommandService;
        private readonly IAuthQueryService _authQueryService;

        public AuthenticationController(IAuthCommandService authCommandService, IAuthQueryService authQueryService)
        {
            _authCommandService = authCommandService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            ErrorOr<AuthenticationResult> authResult = _authCommandService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            ErrorOr<AuthenticationResult> authResult = _authQueryService.Login(request.Email, request.Password);

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
