using Mapster;
using Salon.Application.Authentication.Commands.Register;
using Salon.Application.Authentication.Common;
using Salon.Application.Authentication.Queries.Login;
using Salon.Contracts.Authentication;

namespace Salon.API.Common.Mapping
{
    public class AuthenticationMappingConfig :IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest=>dest, src=>src.User);
        }
    }
}
