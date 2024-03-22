using Hope.Core.Dtos;
using Hope.Core.Features.CommentOperation.Queries.GetReplies;
using Hope.Core.Features.PostOperation.Queries.GetAllPosts;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfThings;
using Hope.Domain.Model;
using Mapster;

namespace Hope.Core.Common.Mapping
{
    public class PostMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


            // config.NewConfig<GetAllPostsQueryResponse, PostOfLostPeople>();

            config.NewConfig<PostOfLostPeople, GetAllPostsQueryResponse>()
                .Map(dest => dest.UserName, src => src.User.DisplayName ?? src.User.UserName)
                .Map(dest=>dest.UserImage,src=>src.User.UserImage);


            config.NewConfig<PostOfLostPeople, GetAllPostsOfPeopleQueryResponse>().Map(dest => dest.UserName, src => src.User.DisplayName ?? src.User.UserName)
                .Map(dest => dest.UserImage, src => src.User.UserImage);
           

            config.NewConfig<PostOfLostThings, GetAllPostsOfThingsQueryResponse>().Map(dest => dest.UserName, src => src.User.DisplayName ?? src.User.UserName)
                .Map(dest => dest.UserImage, src => src.User.UserImage);
            

            //config.NewConfig<PostThingsRequest, PostOfLostThings>();

            //config.NewConfig<UpdatePostOfPeopleRequest, PostOfLostPeople>();
            // config.NewConfig<CommentRequest, Comment>();
            //onfig.NewConfig<AddingCommentToCommentRequest, Comment>();
            //config.NewConfig<Comment, CommentResponse>();
            config.NewConfig<Comment, GetRepliesQueryResponse>().Map(i => i, i => i.User);

            config.NewConfig<PostOfLostThings, AiPostThingsResposnse>().Map(dest => dest.UserName, src => src.User.DisplayName ?? src.User.UserName);
            config.NewConfig<PostOfLostPeople, AiPostPeopleResposnse>().Map(dest => dest.UserName, src => src.User.DisplayName ?? src.User.UserName);

        }
    }
}
