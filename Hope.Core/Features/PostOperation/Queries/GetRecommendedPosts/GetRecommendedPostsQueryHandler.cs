using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPosts;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetRecommendedPosts
{
    internal class GetRecommendedPostsQueryHandler : IRequestHandler<GetRecommendedPostsQuery, Response>
    {
        private readonly IRecommendationService recommendationService;
        private readonly IUnitofWork work;
        private IStringLocalizer<GetRecommendedPostsQueryHandler> localizer;

        public GetRecommendedPostsQueryHandler(IRecommendationService recommendationService, IUnitofWork work, IStringLocalizer<GetRecommendedPostsQueryHandler> localizer)
        {
            this.recommendationService = recommendationService;
            this.work = work;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetRecommendedPostsQuery query, CancellationToken cancellationToken)
        {
            var location=await work.Repository<Location>().GetItem(i=>i.City==query.City);  

            var cluster=await recommendationService.predict(location.Longitude, location.Latitude);   

            var peopleposts= work.Repository<PostOfLostPeople>().Get(i=>i.Cluster==cluster).Result.ToList();   
            var Thingposts= work.Repository<PostOfLostThings>().Get(i=>i.Cluster==cluster).Result.ToList();

            List<GetAllPostsQueryResponse> allposts = [..peopleposts?.Adapt<List<GetAllPostsQueryResponse>>(), .. Thingposts?.Adapt<List<GetAllPostsQueryResponse>>()]; 

            return await Response.SuccessAsync(allposts, localizer["Success"].Value); 
        }
    }
}
