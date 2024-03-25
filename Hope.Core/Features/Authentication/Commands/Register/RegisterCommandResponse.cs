using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Commands.Register
{
    public class RegisterCommandResponse
    {
        public bool IsAuthenticated { get; set; } = false;
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
    }
}
