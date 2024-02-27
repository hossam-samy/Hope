using Hope.Core.Dtos;
using Hope.Domain.Model;
using Mapster;

namespace Hope.Core.Common.Mapping
{
    public class UserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserDto, User>();

        }
    }
}
