using Hope.Core.Features.CommentOperation.Queries.GetReplies;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents;
using Hope.Domain.Model;
using Mapster;

namespace Hope.Core.Common.Mapping
{
    public class PostMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


           // config.NewConfig<GetAllPostsOfAccidentsQueryResponse, PostOfLostPeople>();


            config.NewConfig<PostOfLostPeople, GetAllPostsOfPeopleQueryResponse>().Map(dest => dest.UserName, src => src.User.DisplayName??src.User.UserName);

            config.NewConfig<PostOfLostThings, GetAllPostsOfPeopleQueryResponse>().Map(dest => dest.UserName, src => src.User.DisplayName ?? src.User.UserName);


            //config.NewConfig<PostThingsRequest, PostOfLostThings>();

            //config.NewConfig<UpdatePostOfPeopleRequest, PostOfLostPeople>();
           // config.NewConfig<CommentRequest, Comment>();
            //onfig.NewConfig<AddingCommentToCommentRequest, Comment>();
            //config.NewConfig<Comment, CommentResponse>();
            config.NewConfig<Comment, GetRepliesQueryResponse>().Map(i=>i.Comments,i=>i.Comments);

        }
    }
}
