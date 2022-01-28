using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IDSEmpty
{
    public class LoginForm
    {
        [Required]
        [StringLength(20, ErrorMessage = "Value is too large.")]
        public string Username { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Value is too large.")]
        public string Password { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Value is too large.")]
        public string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Value is too large.")]
        public string Name { get; set; }

    }
}
