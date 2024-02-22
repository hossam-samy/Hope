using Hope.Core.Dtos;
using Hope.Domain.Model;
using Mapster;

namespace Hope.Core.Mapping
{
    public class PostMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig< PostPeopleRequest, PostOfLostPeople>().Ignore(i=>i.Image);

            config.NewConfig<PostPeopleRequest, PostOfLostPeople>().
                Map(dest=>dest.ImageUrl,src=>src.ImageFile.FileName);

            config.NewConfig<PostOfLostPeople, PostPeopleResponse>();
                  //.Map(i => i.Condition, i => i.Condition)
            //    //.Map(i => i.Age,i => i.Age)
            //    //.Map(i => i.Description,i => i.Description)
            //    //.Map(i => i.Gendre,i => i.Gendre)
            //    //.Map(i => i.Image,i => i.Image)
            //    //.Map(i => i.Name, i => i.Name)
            //    //.Map(i => i.UserId, i => i.UserId);
            config.NewConfig<PostOfLostThings, PostDto>();
            //    //.Map(i => i.Description, i => i.Description)
            //    //.Map(i => i.Image, i => i.Image)
            //    //.Map(i => i.Type, i => i.Type)
            //    //.Map(i => i.UserId, i => i.UserId);
            config.NewConfig<PostDto, PostOfLostThings>();
        }
    }
}
