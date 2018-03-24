using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlServerBookStore.Models
{
    public partial class BookAuthor
    {
        public int BookAuthorID { set; get; }

        [ForeignKey("Book")]
        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "{0} tidak boleh lebih {1} dan tidak boleh kurang {2} karakter.")]
        public int BookID { set; get; }
        public Book Book { get; set; }

        [ForeignKey("Author")]
        [Display(Name = "AuthorID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public int AuthorID { set; get; }
        public Author Author { get; set; }
    }
}
