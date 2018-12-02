using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Author
    {
        public Author()
        {
            BlogPosts = new HashSet<BlogPost>();
        }

        public int ID { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}