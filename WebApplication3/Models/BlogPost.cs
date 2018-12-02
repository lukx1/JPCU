using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class BlogPost
    {
        public int ID { get; set; }
        public int IDAuthor { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }

        public virtual Author Author { get; set; }
    }
}