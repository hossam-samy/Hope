using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Interfaces
{
    public interface IMediaService
    {

        Task<string> AddFileAsync(IFormFile file,string dest,string name);
        Task DeleteFileAsync(string url);   
        Task<string> UpdateFileAsync(string url,IFormFile file,string dest, string name);   
        }
}
