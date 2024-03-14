using Hope.Core.Common;
using Hope.Core.Dtos;
using Microsoft.AspNetCore.Http;

namespace Hope.Core.Interfaces
{
    public interface IAuthService
    {
        Task<Response> UserRegister(UserDto member);
        //Task<AuthModel> AdminRegister(UserDto member);
        Task<Response> Login(LoginRequest model);

        Task<Response> ChangePassword(string UserEmail, string password);

        Task<Response> GetAllUsers();

        Task<Response> GetProfile(string UserId);

        Task<Response>AddUserImage(AddImageRequest request);

    }
}
