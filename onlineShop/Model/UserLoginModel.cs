using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Model
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(24)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(24)]
        public string Password { get; set; }
    }
}
