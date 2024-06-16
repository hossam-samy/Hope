using Hope.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
