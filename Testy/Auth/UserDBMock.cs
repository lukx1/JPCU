using libcommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testy.Auth
{
    public class UserDBMock : IUserDB
    {
        public List<string[]> Users { get; private set; } = new List<string[]>(3);
        public UserDBMock()
        {
            Users.Add(
                new string[] { "1", "Foo", "comop", "Admin", "Foo", "Bar" }
                );
            Users.Add(
                new string[] { "2", "Yar", "kesjep", "Member", "Yaros", "Takos" }
                );
            Users.Add(
                new string[] { "3", "UAch", "oooy", "Member", "Jach", "Dach" }
                );
        }
        public List<string[]> FetchUsers()
        {
            return Users;
        }
    }
}
