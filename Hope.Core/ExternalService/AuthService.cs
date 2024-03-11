using Hope.Core.Common;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hope.Core.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<AuthService> localizer;
        private readonly IUnitofWork unitofWork;
        private readonly IMediaService mediaService;

        public AuthService(UserManager<User> userManager, IMapper mapper, IStringLocalizer<AuthService> localizer, IConfiguration configuration, IUnitofWork unitofWork, IMediaService mediaService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.localizer = localizer;
            this.configuration = configuration;
            this.unitofWork = unitofWork;
            this.mediaService = mediaService;
        }


        public async Task<Response> GetProfile(string UserId)
        {
            var user =await userManager.FindByIdAsync(UserId);  

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"]);
            }


            List<PostDto> allposts = [.. user.lostPeople.Adapt<List<PostDto>>(), .. user.lostThings.Adapt<List<PostDto>>()];


            return await Response.SuccessAsync(new { user.Name, user.City, allposts }, localizer["Success"]);

        }



        public async Task<Response> AddUserImage(AddImageRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            user.UserImage = await mediaService.AddFileAsync(request.Image, "User", user.UserName);

            await unitofWork.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);

        }

        public async Task<Response> ChangePassword(string UserEmail, string password)
        {
            var user = await userManager.FindByEmailAsync(UserEmail);


            if (user is null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            var result = await userManager.RemovePasswordAsync(user);


            if (!result.Succeeded)
                return await Response.FailureAsync(localizer["Faild"]);


            var identityResult = await userManager.AddPasswordAsync(user, password);


            if (!identityResult.Succeeded)
                return await Response.FailureAsync(localizer["Faild"]);

            return await Response.SuccessAsync("good");
        }

        public async Task<Response> Login(LoginRequest model)
        {

            var user = await userManager.FindByEmailAsync(model.Email!);


            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password!))
            {
                return await Response.FailureAsync(localizer["InvalidLogin"].Value);
            }

            var token = await CreateJwtToken(user);

            var role = await userManager.GetRolesAsync(user);

            var login = new LoginResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Roles = role.ToList(),
                Name = user.Name,
                IsAuthenticated = true,
            };

            return await Response.SuccessAsync(login, localizer["Success"].Value);
        }

        public async Task<Response> SearchForUsers(string name)
        {
            var user =  unitofWork.Repository<User>().Get(i => i.Name.Contains(name)).Result.Select(i =>new {i.Name,i.Id});

            return await Response.SuccessAsync(user);
        }

        public async Task<Response> UserRegister(UserDto model)
        {

            if (await userManager.FindByEmailAsync(model.Email) is not null)
                return await Response.FailureAsync(localizer["EmailExist"].Value);



            var user = mapper.Map<User>(model);



            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors, localizer["Faild"].Value);
            }

            var res = await userManager.AddToRoleAsync(user, "User");

            if (!res.Succeeded)
            {
                return await Response.FailureAsync(result.Errors, localizer["Faild"].Value);
            }
            var jwtsecuritytoken = await CreateJwtToken(user);


            return await Response.SuccessAsync(new LoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                IsAuthenticated = true,
                Username = user.UserName,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken),
                Name = user.Name,
            }, localizer["Success"].Value);
        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDay"]!)),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }


    }
}
