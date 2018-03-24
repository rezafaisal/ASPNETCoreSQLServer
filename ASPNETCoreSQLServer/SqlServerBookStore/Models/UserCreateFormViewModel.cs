using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SqlServerBookStore.Models
{
    public partial class UserCreateFormViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public String UserName { set; get; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string[] RoleID { set; get; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "{0} tidak valid.")]
        public String Email { set; get; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        [DataType(DataType.Password)]
        public String Password { set; get; }

        [Display(Name = "Password Confirm")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        [Compare("Password", ErrorMessage = "{0} dan {1} harus sama.")]
        [DataType(DataType.Password)]
        public String PasswordConfirm { set; get; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        public String FullName { set; get; }
    }
}
