using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int IDCategory { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}