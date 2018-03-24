using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SqlServerBookStore.Models
{
    public partial class BookFormViewModel
    {
        [Display(Name = "ISBN")]
        public int ISBN { set; get; }

        [Display(Name = "Category")]
        public int CategoryID { set; get; }

        [Display(Name = "Title")]
        public String Title { set; get; }

        [Display(Name = "Photo")]
        [DataType(DataType.Upload)]
        public IFormFile Photo { set; get; }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { set; get; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public double Price { set; get; }

        [Display(Name = "Quantity")]
        public int Quantity { set; get; }

        [Display(Name = "List of Author Names")]
        public int[] AuthorIDs { set; get; }
    }
}
