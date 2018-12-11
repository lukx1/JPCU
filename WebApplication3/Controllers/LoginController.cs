using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Misc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class LoginController : Controller
    {
        private MyContext sql = new MyContext();

        public LoginController()
        {
            /*var users = sql.Users.ToList();
            users[0].Password = PasswordFactory.HashPasswordPbkdf2("123456");
            users[1].Password = PasswordFactory.HashPasswordPbkdf2("123456");
            sql.SaveChanges();*/
        }

        private ActionResult IndexRoutine()
        {
            var sessionManager = new SessionManager(this);
            if (sessionManager.IsUserLoggedIn())
            {
                ViewBag.Status = "Logged in as " + sessionManager.LoggedInUser.Name;
            }
            return View();
        }

        // GET: Login
        public ActionResult Index()
        {
            return IndexRoutine();
        }

        [HttpPost][HttpGet]
        public ActionResult Login()
        {
            return IndexRoutine();
        }


        [HttpPost]
        public ActionResult Login(string name, string pass)
        {
            var user = sql.Users.Where(r => r.Login == name).FirstOrDefault();
            if(user == null)
            {
                ViewBag.Status = "User not found";
                return View("Index");
            }

            if (!user.HasPermission(Misc.Permission.Login))
            {
                ViewBag.Status = "You don't have the required permissions";
            }
            else if (!PasswordFactory.ComparePasswordsPbkdf2(pass, user.Password))
            {
                ViewBag.Status = "Incorrect name or password";
            }
            else
            {
                var sessionManager = new SessionManager(this);
                sessionManager.LoggedInUser = user;
                ViewBag.Status = "Logged in as "+user.Name;
            }

            return View("Index");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
