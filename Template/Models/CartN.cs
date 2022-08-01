using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Template.Models
{
    public class CartN
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Brand { get; set; }
        public string Price { get; set; }
        public string Total { get; set; }
        public string qty { get; set; }
        public string Image { get; set; }
    }
}