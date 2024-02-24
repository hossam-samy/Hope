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
        Task AddFileAsync(IFormFile file);
        string GetUrl();
    }
}
