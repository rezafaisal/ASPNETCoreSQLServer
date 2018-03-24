using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SqlServerBookStore.Models
{
    public class ContohModel
    {
        [Display(Name = "Contoh Text")]
        public String ContohText { set; get; }

        [Display(Name = "Contoh Date Time")]
        public DateTime ContohDateTime { set; get; }

        [Display(Name = "Contoh Number")]
        public Double ContohNumber { set; get; }

        [Display(Name = "Contoh Boolean")]
        public Boolean ContohBoolean { set; get; }
    }
}
