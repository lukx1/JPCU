using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libcommon
{
    public interface IUserDB
    {
        List<string[]> FetchUsers();
    }
}
