using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Misc
{
    public class SessionManager
    {
        private HttpSessionStateBase Session;
        public SessionManager(Controller controller)
        {
            this.Session = controller.Session;
        }

        public SessionManager(HttpSessionStateBase sessionStateBase)
        {
            this.Session = sessionStateBase;
        }

        public User LoggedInUser
        {
            get
            {
                return Session != null ? (User)Session["user"] : null;
            }
            set
            {
                Session["user"] = value;
            }
        }

        public bool IsUserLoggedIn()
        {
            return LoggedInUser != null;
        }
    }
}