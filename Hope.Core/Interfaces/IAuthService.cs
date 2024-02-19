using Hope.Core.Dtos;
using Hope.Domain.Common;

namespace Hope.Core.Interfaces
{
    public interface IAuthService
    {
        Task<Response> UserRegister(UserDto member);
        //Task<AuthModel> AdminRegister(UserDto member);
        Task<Response> Login(LoginRequest model);    
    }
}
