using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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
            if (File.Exists(url))
            {
                File.Delete(url);
            }

            return Task.CompletedTask;  
        }

        public string GetUrl() =>  _host.WebRootPath;

        
        public async Task UpdateFileAsync(string url, IFormFile file,string dest, string name)
        {
            await DeleteFileAsync(url);
            await AddFileAsync(file,dest,name);   
            
        }
    }
}
