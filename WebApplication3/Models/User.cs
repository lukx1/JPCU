using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Misc;

namespace WebApplication3.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Permission Permissions { get; set; }
        public string Name { get; set; }

        public bool HasPermission(Permission permission)
        {
            return Permissions.HasFlag(permission);
        }
    }
}