using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlServerBookStore.Models
{
    public partial class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "{0} tidak boleh lebih {1} dan tidak boleh kurang {2} karakter.")]
        public int BookID { set; get; }

        [ForeignKey("Category")]
        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        public int CategoryID { set; get; }

        public Category Category { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public String Title { set; get; }

        [Display(Name = "Photo")]
        public String Photo { set; get; }

        [Display(Name = "Publish Date")]
        public DateTime PublishDate { set; get; }

        [Display(Name = "Price")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        public double Price { set; get; }

        [Display(Name = "Quantity")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        public int Quantity { set; get; }

        public ICollection<BookAuthor> BooksAuthors { get; set; }
    }
}
