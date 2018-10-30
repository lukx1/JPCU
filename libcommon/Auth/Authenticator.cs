using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libcommon
{
    public class Authenticator : IAuthenticator
    {
        public IUserDB UserDB { get; set; }

        public Authenticator(IUserDB db)
        {
            UserDB = db;
        }

        private bool ArePasswordSame(string raw, string hashed)
        {
            var rawReversed = new string(raw.Reverse().ToArray());
            return rawReversed == hashed;
        }

        public UserInfo Login(string name, string pass)
        {
            var res = UserDB.FetchUsers().Where(r => r[1] == name);
            if (res.Count() < 1)
                throw new InvalidUserException($"User {name} not found");
            if (res.Count() > 1)
                throw new InvalidUserException("Multiple users with same name found");

            var user = res.FirstOrDefault();

            if (ArePasswordSame(pass, user[2])){
                return new UserInfo()
                {
                    DisplayName = user[4] + " " + user[5],
                    ID = int.Parse(user[0]),
                    Role = user[3]
                };
            }
            throw new InvalidPasswordException($"Invalid password for user {name}");
        }
    }
}
