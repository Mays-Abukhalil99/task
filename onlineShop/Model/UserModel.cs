using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Model
{
    public class UserModel
    {
        [Required(ErrorMessage = "name is required")]
        [StringLength(24)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(24)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(24)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(24)]
        public string Password { get; set; }
 

    }
}
