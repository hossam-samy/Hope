﻿using Hope.Core.Dtos;
using Hope.Domain.Model;
using Mapster;

namespace Hope.Core.Common.Mapping
{
    public class PostMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


            config.NewConfig<PostPeopleRequest, PostOfLostPeople>();


            config.NewConfig<PostOfLostPeople, PostPeopleResponse>().Map(dest => dest.UserName, src => src.User.Name??src.User.UserName);

            config.NewConfig<PostOfLostThings, PostThingResponse>().Map(dest => dest.UserName, src => src.User.Name ?? src.User.UserName);


            config.NewConfig<PostThingsRequest, PostOfLostThings>();

            config.NewConfig<UpdatePostOfPeopleRequest, PostOfLostPeople>();
            config.NewConfig<CommentRequest, Comment>();
            config.NewConfig<AddingCommentToCommentRequest, Comment>();
            config.NewConfig<Comment, CommentResponse>();
            config.NewConfig<Comment, RepliesDto>().Map(i=>i.Comments,i=>i.Comments);

        }
    }
}
