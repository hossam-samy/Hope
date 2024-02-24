using Hope.Core.Dtos;
using Hope.Domain.Model;
using Mapster;

namespace Hope.Core.Mapping
{
    public class PostMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            

            config.NewConfig<PostPeopleRequest, PostOfLostPeople>();

            


            config.NewConfig<PostThingsRequest, PostOfLostThings>();
        }
    }
}
