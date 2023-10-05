using ErrorOr;
using MapsterMapper;
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
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            //new RegisterCommand(request.FirstName,request.LastName, request.Email, request.Password);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request); //new LoginQuery(request.Email, request.Password);
            var authResult = await _mediator.Send(query);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }

        //public static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        //{
        //    return new AuthenticationResponse(
        //        authResult.User.Id,
        //        authResult.User.FirstName,
        //        authResult.User.LastName,
        //        authResult.User.Email,
        //        authResult.Token
        //        );
        //}
    }

}
