using Hope.Core.Dtos;
using Hope.Domain.Model;
using Mapster;

namespace Hope.Core.Common.Mapping
{
    public class PostMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


            config.NewConfig<PostPeopleRequest, PostOfLostPeople>();


            config.NewConfig<PostOfLostPeople, PostThingResponse>().Map(dest => dest.UserName, src => src.User.UserName);

            config.NewConfig<PostOfLostThings,PostThingResponse>().Map(dest=>dest.UserName,src=>src.User.UserName);


            config.NewConfig<PostThingsRequest, PostOfLostThings>();
        }
    }
}
