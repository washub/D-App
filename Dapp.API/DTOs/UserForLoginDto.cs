using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dapp.API.DTOs
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength =4, ErrorMessage ="Your password must have chars between 4 to 8...")]
        public string Password { get; set; }
    }
}