using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libcommon
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException(string s) : base(s) { }
    }
}
