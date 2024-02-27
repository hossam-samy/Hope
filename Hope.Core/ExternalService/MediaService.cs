using Hope.Core.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Service
{
    public class MediaService : IMediaService
    {
        private readonly IWebHostEnvironment _host;

        public MediaService(IWebHostEnvironment host)
        {
            _host = host;
        }

        public async Task AddFileAsync(IFormFile file, string name)
        {
            if (file.Length > 0)
            {
               
                string filepath = Path.Combine(_host.WebRootPath, @"PostsImages");
                using (var fileStream = new FileStream(Path.Combine(filepath,name), FileMode.OpenOrCreate))
                {
                    await file.CopyToAsync(fileStream);
                    
                }

            }
        }

        public Task DeleteFileAsync(string url)
        {
            File.Delete(url);   

            return Task.CompletedTask;  
        }

        public string GetUrl() =>  _host.WebRootPath;

        public Task UpdateDeleteFileAsync(string url, IFormFile file, string name)
        {
            DeleteFileAsync(url);
            AddFileAsync(file, name);   
            return Task.CompletedTask;  
        }
    }
}
