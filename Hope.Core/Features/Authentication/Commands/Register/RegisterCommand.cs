﻿using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Commands.Register
{
    public class RegisterCommand:IRequest<Response>
    {
        public string UserName { get; set; }
        public string Name { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}