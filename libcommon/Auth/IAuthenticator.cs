using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libcommon
{
    public interface IAuthenticator
    {
        IUserDB UserDB { get; set; }
        UserInfo Login(string userName, string password);
    }
}
