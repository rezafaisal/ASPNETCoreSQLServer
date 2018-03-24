using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SqlServerBookStore.Models
{
    public class UserLoginFormViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public String UserName { set; get; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DataType(DataType.Password)]
        public String Password { set; get; }
    }
}
