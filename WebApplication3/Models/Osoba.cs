using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Osoba
    {
        public int OsobaId { get; set; }

        public string Jmeno { get; set; }

        public string Prijmeni { get; set; }

        public int Vek { get; set; }
    }
}