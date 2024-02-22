﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class UserDto
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        //[RegularExpression("01[125][0-9]{8}",ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        //[EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
