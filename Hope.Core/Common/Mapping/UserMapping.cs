using Hope.Core.Features.Authentication.Commands.Register;
using Hope.Domain.Model;
using Mapster;

namespace Hope.Core.Common.Mapping
{
    public class UserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterCommand, User>();

        }
    }
}
