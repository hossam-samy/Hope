using Hope.Core.Common;
using Hope.Core.Dtos;
using Hope.Domain.Model;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Hope.Core.Interfaces
{
    public interface IPostService
    {
        public Task<Response> AddPostPeople(PostPeopleRequest dto);
        public Task<Response> AddPostThings(PostThingsRequest dto);
        public Task<Response> GetPostThings();
        public Task<Response> GetAllPosts();
        public Task<Response> GetPostOfShelters();
        public Task<Response> GetPostOfAccidents();
        public Task<Response> GetPostOfLosties();
        public  Task<Response> DeleteFileAsync(string url);
        public  Task<Response> DeletePost(ServiceRequests requests);

        public  Task<Response> UpdatePostOfThingsPost(UpdatePostOfThingsRequest request);
        public  Task<Response> UpdatePostOfPeoplePost(UpdatePostOfPeopleRequest request);
        public  Task<Response> PinPost<T>(string UserId,int PostId)where T:Post;

        public Task<Response> UnPinPost<T>(string UserId, int PostId)where T:Post;
        public  Task<Response> HidePost<T>(string UserId,int PostId)where T:Post;
        public Task<Response> AddCommentToPost<T>(CommentRequest request) where T : Post;
        public  Task<Response> AddCommentToComment(AddingCommentToCommentRequest request);
        public Task<Response> UpdateComment(UpdateCommentRequest request);
        public Task<Response> DeleteComment(int id);



    }
}
