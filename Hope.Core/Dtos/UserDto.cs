using System;
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
        [RegularExpression("01[0125][0-9]{8}",ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("@gmail\\.com$",ErrorMessage ="Invalid Email")]
        
        public string Email { get; set; }
        [Required]
        [MinLength(8,ErrorMessage ="Password should be at least 8 chars")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
