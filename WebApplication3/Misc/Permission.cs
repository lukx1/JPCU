using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Misc
{
    [Flags]
    public enum Permission : Int32
    {
        None = 0,
        Login = 1 << 0,
        Edit_Blogs = 1 << 1,
        Third = 1 << 2,
        Fourth = 1 << 3
    }
}