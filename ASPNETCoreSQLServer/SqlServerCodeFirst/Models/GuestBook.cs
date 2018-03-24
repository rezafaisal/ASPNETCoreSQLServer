using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlServerCodeFirst.Models
{
    public class GuestBook
    {
        public int Id { set; get; }
        public String Name { set; get; }
        public String Email { set; get; }
        public String Message { set; get; }

    }
}
