using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.Login
{
    public class LoginQueryResponse
    {
        public bool IsAuthenticated { get; set; } = false;
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? UserImage { get; set; }
        public string? City { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
    }
}
