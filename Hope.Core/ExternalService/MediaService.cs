using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Hope.Core.Service
{
    public class MediaService : IMediaService
    {
        private readonly IWebHostEnvironment _host;
        private readonly HttpContext _httpContext;

        public MediaService(IWebHostEnvironment host, IHttpContextAccessor httpContext)
        {
            _host = host;
            _httpContext = httpContext.HttpContext;
        }


        public async Task<string> AddFileAsync(IFormFile media,string dest,string name)
        {
            if(media == null || media.Length == 0) { 
              
                return string.Empty;    
            }

            var extension = Path.GetExtension(media.FileName);

            var uniqueFileName = name + extension;


            var uploadsFolder = Path.Combine("wwwroot", $"{dest}Images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                media.CopyTo(stream);
            }

            var baseUrl = @$"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}/{dest}Images/";

            return baseUrl + uniqueFileName;
        }
        

        public Task DeleteFileAsync(string url)
        {
            //https: / / localhost:44318 / PostOfLostThingsImages / 6.png

            if(url.IsNullOrEmpty())
                return Task.CompletedTask;   

            var path = "wwwroot/"+url.Split('/')[3]+ '/' + url.Split('/')[4];
            

            var fullpath = Path.GetFullPath(path);


            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            return Task.CompletedTask;  
        }

        public async Task<string> UpdateFileAsync(string url, IFormFile file,string dest, string name)
        {
            if(file == null || file.Length == 0)   
                return string.Empty;    

            await DeleteFileAsync(url);
            return await AddFileAsync(file,dest,name);   
            
        }
    }
}
