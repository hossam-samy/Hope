using Hope.Core.Common;
using Hope.Core.Common.Consts;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Runtime.InteropServices;


namespace Hope.Core.Service
{
    public class PostService : IPostService
    {
        private readonly IUnitofWork work;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<PostService> localizer;
        private readonly IMediaService mediaService;
        private readonly UserManager<User> userManager;
        public PostService(IUnitofWork work, IMapper mapper, IStringLocalizer<PostService> localizer, IMediaService mediaService, UserManager<User> userManager)
        {
            this.work = work;
            this.mapper = mapper;
            this.localizer = localizer;
            this.mediaService = mediaService;
            this.userManager = userManager;
        }
        public async Task<Response> AddPostPeople([FromForm] PostPeopleRequest dto)
        {


            var post = mapper.Map<PostOfLostPeople>(dto);

            await work.Repository<PostOfLostPeople>().AddAsync(post);


            var result = await mediaService.AddFileAsync(dto.ImageFile, post.GetType().Name, post.Id.ToString());

            post.ImageUrl = result;

            await work.Repository<PostOfLostPeople>().Update(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }

        public async Task<Response> AddPostThings([FromForm] PostThingsRequest dto)
        {

            var post = mapper.Map<PostOfLostThings>(dto);

            await work.Repository<PostOfLostThings>().AddAsync(post);

            var result = await mediaService.AddFileAsync(dto.ImageFile, post.GetType().Name, post.Id.ToString());

            post.ImageUrl = result;

            await work.Repository<PostOfLostThings>().Update(post);

            return await Response.SuccessAsync(localizer["Success"].Value);

        }

        public async Task<Response> GetAllPosts(int? Peoplecursor, int? thingcursor, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            var peopleposts = Peoplecursor != 0 ? work.Repository<PostOfLostPeople>().
                Get(i => i.Id > Peoplecursor && !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" }).Result.Take(16).ToList().Adapt<List<PostDto>>() : new List<PostDto>();

            var thingsposts = thingcursor != 0 ? work.Repository<PostOfLostThings>().
               Get(i => i.Id > thingcursor &&!i.HiddenThings.Contains(user), new[] { "HiddenThings" }).Result.Take(16).ToList().Adapt<List<PostDto>>() : new List<PostDto>();




            Peoplecursor = peopleposts?.LastOrDefault()?.Id;
            thingcursor = thingsposts?.LastOrDefault()?.Id;


            List<PostDto> allposts = [.. peopleposts, .. thingsposts];

            //post1.ToList().ForEach(x => x.UserName = Peopleposts.Select(i => i.Name).FirstOrDefault() ?? x.UserName);


            return await Response.SuccessAsync(new { allposts, Peoplecursor, thingcursor }, localizer["Success"]);
        }
        public async Task<Response> GetPostOfAccidents(int? cursor, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);

            var posts = cursor != 0 ? work.Repository<PostOfLostPeople>().
               Get(i => i.Id > cursor && i.Condition==Condition.accidents && !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" }).Result.Take(32).ToList().Adapt<List<PostPeopleResponse>>() : new List<PostPeopleResponse>();

            cursor = posts?.LastOrDefault()?.Id;

            return await Response.SuccessAsync(new { posts ,cursor}, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfLosties(int? cursor, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            var posts = cursor != 0 ? work.Repository<PostOfLostPeople>().
             Get(i => i.Id > cursor && i.Condition == Condition.losties && !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" }).Result.Take(32).ToList().Adapt<List<PostPeopleResponse>>() : new List<PostPeopleResponse>();


            cursor = posts?.LastOrDefault()?.Id;

            return await Response.SuccessAsync(new { posts, cursor }, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfShelters(int? cursor, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            var posts = cursor != 0 ? work.Repository<PostOfLostPeople>().
             Get(i => i.Id > cursor && i.Condition == Condition.shelters && !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" }).Result.Take(32).ToList().Adapt<List<PostPeopleResponse>>() : new List<PostPeopleResponse>();


            cursor = posts?.LastOrDefault()?.Id;

            return await Response.SuccessAsync(new { posts, cursor }, localizer["Success"].Value);
        }


        public async Task<Response> GetPostThings(int? cursor, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            var posts = cursor != 0 ? work.Repository<PostOfLostThings>().
                Get(i => i.Id > cursor && !i.HiddenThings.Contains(user), new[] { "HiddenThings" }).Result.Take(32).ToList().Adapt<List<PostThingResponse>>() : new List<PostThingResponse>();


            cursor = posts?.LastOrDefault()?.Id;

            return await Response.SuccessAsync(new { posts, cursor }, localizer["Success"].Value);
        }
        public async Task<Response> DeleteFileAsync(string url)
        {
            await mediaService.DeleteFileAsync(url);
            return await Response.SuccessAsync("tmam");
        }








        public async Task<Response> DeletePost(DeletePostRequests requests)
        {
            var user = await userManager.FindByIdAsync(requests.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            Post? post;
            if (requests.IsPeople)
            {
                post = user.lostPeople.FirstOrDefault();
                if (post == null)
                {
                    return await Response.FailureAsync("You are not allowed to delete this post");

                }
                var peopleposts = (PostOfLostPeople)post;

                foreach (var item in peopleposts?.Comments)
                {
                    await DeleteComment(item.Id);
                }

                await mediaService.DeleteFileAsync(post.ImageUrl);
                await work.Repository<PostOfLostPeople>().Delete((PostOfLostPeople)post);
                return await Response.SuccessAsync(localizer["Success"]);
            }
            post = user.lostThings.FirstOrDefault();
            if (post == null)
            {
                return await Response.FailureAsync("PostNotExist");

            }
            var Thingsposts = (PostOfLostThings)post;

            foreach (var item in Thingsposts?.Comments)
            {
                await DeleteComment(item.Id);
            }
            await mediaService.DeleteFileAsync(post.ImageUrl);
            await work.Repository<PostOfLostThings>().Delete((PostOfLostThings)post);
            return await Response.SuccessAsync(localizer["Success"]);

        }
        public async Task<Response> UpdatePostOfPeoplePost(UpdatePostOfPeopleRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            var post = user.lostPeople.FirstOrDefault(i => i.Id == request.Id);

            if (post == null) return await Response.FailureAsync("You are not allowed to update this post");


            post.IsSearcher = request.IsSearcher ?? post.IsSearcher;
            post.Age = request.Age ?? post.Age;
            post.Name = request.Name ?? post.Name;
            post.City = request.City ?? post.City;
            post.Condition = request.Condition ?? post.Condition;
            post.Description = request.Description ?? post.Description;

            var newUrl = await mediaService.UpdateFileAsync(post.ImageUrl, request.Image, "PostOfLostPeople", post.Id.ToString());

            post.ImageUrl = newUrl == string.Empty ? post.ImageUrl : newUrl;
            post.MissigDate = request.MissigDate ?? post.MissigDate;
            post.PhoneNumber = request.PhoneNumber ?? post.PhoneNumber;
            post.Town = request.Town ?? post.Town;
            post.Gendre = request.Gendre ?? post.Gendre;

            await work.Repository<PostOfLostPeople>().Update(post);



            return await Response.SuccessAsync(localizer["Success"]);

        }
        public async Task<Response> UpdatePostOfThingsPost(UpdatePostOfThingsRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            var post = user.lostThings.FirstOrDefault(i => i.Id == request.Id);

            if (post == null) return await Response.FailureAsync("You are not allowed to update this post");



            post.IsSearcher = request.IsSearcher ?? post.IsSearcher;
            post.Type = request.Type ?? post.Type;
            post.City = request.City ?? post.City;
            post.Description = request.Description ?? post.Description;

            var newUrl = await mediaService.UpdateFileAsync(post.ImageUrl, request.Image, "PostOfLostPeople", post.Id.ToString());

            post.ImageUrl = newUrl == string.Empty ? post.ImageUrl : newUrl;
            post.MissigDate = request.MissigDate ?? post.MissigDate;
            post.PhoneNumber = request.PhoneNumber ?? post.PhoneNumber;
            post.Town = request.Town ?? post.Town;



            await work.Repository<PostOfLostThings>().Update(post);

            return await Response.SuccessAsync(localizer["Success"]);


        }





        public async Task<Response> PinPost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }


            Post? post = work.Repository<T>().Get(i => i.Id == PostId).Result.FirstOrDefault();
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"]);

            }
            if (typeof(T).Name == nameof(PostOfLostPeople))
            {

                user.PinningPeoples.Add((PostOfLostPeople)post);

            }
            else
            {

                user.PinningThings.Add((PostOfLostThings)post);

            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);
        }
        public async Task<Response> UnPinPost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"]);
            }


            Post? post = work.Repository<T>().Get(i => i.Id == PostId).Result.FirstOrDefault();
            if (post == null)
            {
                return await Response.FailureAsync(localizer["PostNotExist"]);
            }

            if (!user.PinningPeoples.Any(i => i == post) && !user.PinningThings.Any(i => i == post))
            {
                return await Response.FailureAsync("UnPinError");
            }

            if (typeof(T).Name == nameof(PostOfLostPeople))
            {
                user.PinningPeoples.Remove((PostOfLostPeople)post);
            }
            else
            {
                user.PinningThings.Remove((PostOfLostThings)post);
            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);
        }

        public async Task<Response> HidePost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            Post? post = work.Repository<T>().Get(i => i.Id == PostId).Result.FirstOrDefault();
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"]);

            }


            if (user.PinningPeoples.Any(i => i == post) || user.PinningThings.Any(i => i == post))
            {

                return await Response.FailureAsync("HideError");
            }


            if (typeof(T).Name == nameof(PostOfLostPeople))
            {

                user.HiddingPeoples.Add((PostOfLostPeople)post);

            }
            else
            {

                user.HiddingThings.Add((PostOfLostThings)post);

            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);
        }

        public async Task<Response> AddCommentToPost<T>(CommentRequest request) where T : Post
        {


            Post? post = work.Repository<T>().Get(i => i.Id == request.PostId).Result.FirstOrDefault();
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"]);

            }

            var commnet = mapper.Map<Comment>(request);

            if (typeof(T).Name == nameof(PostOfLostPeople))
            {


                commnet.Peopleid = request.PostId;

                var people = (PostOfLostPeople)post;

                people.Comments.Add(commnet);


            }
            else
            {
                commnet.Thingsid = request.PostId;

                var Thing = (PostOfLostThings)post;

                Thing.Comments.Add(commnet);

            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);

        }
        public async Task<Response> AddCommentToComment(AddingCommentToCommentRequest request)
        {
            Comment? comment = work.Repository<Comment>().Get(i => i.Id == request.CommentId).Result.FirstOrDefault();
            if (comment == null)
            {

                return await Response.FailureAsync(localizer["CommentNotExist"]);

            }

            var newcomment = mapper.Map<Comment>(request);

            comment.Comments.Add(newcomment);

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);

        }

        public async Task<Response> UpdateComment(UpdateCommentRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            var comment = user.Comments.FirstOrDefault(i => i.Id == request.CommentId);

            if (comment == null) return await Response.FailureAsync("You are not allowed to update this Comment");

            comment.Content = request.Content;

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);

        }
        public async Task<Response> DeleteComment(DeleteCommentRequests requests)
        {
            var user = await userManager.FindByIdAsync(requests.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            var comment = user.Comments.FirstOrDefault(i => i.Id == requests.CommentId);

            if (comment == null) return await Response.FailureAsync("You are not allowed to delete this Comment");


            await work.Repository<Comment>().Delete(comment);


            return await Response.SuccessAsync(localizer["Success"]);

        }

        private async Task DeleteComment(int id)
        {
            var comment = work.Repository<Comment>().Get(i => i.Id == id).Result.FirstOrDefault();

            await work.Repository<Comment>().Delete(comment);



        }
        public async Task<Response> GetReplies(int id)
        {
            var comment = work.Repository<Comment>().Get(i => i.Id == id).Result.FirstOrDefault();

            if (comment is null)
            {
                return await Response.FailureAsync(localizer["Faild"]);
            }

            var response = mapper.Map<RepliesDto>(comment);



            return await Response.SuccessAsync(response, localizer["Success"]);

        }






    }
}
