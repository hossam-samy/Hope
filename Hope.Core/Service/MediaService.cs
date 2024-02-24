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

        public async Task AddFileAsync(IFormFile file)
        {
            if (file.Length > 0)
            {

                string filepath = Path.Combine(_host.WebRootPath, file.FileName);
                using (var fileStream = new FileStream(filepath, FileMode.OpenOrCreate))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        public string GetUrl() =>  _host.WebRootPath;
       
    }
}
