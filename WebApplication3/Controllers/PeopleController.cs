using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class PeopleController : Controller
    {
        private PeopleRepository peopleRepository = new PeopleRepository();

        public ActionResult Index()
        {
            ViewBag.People = peopleRepository.FindAll();

            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Person = peopleRepository.FindById(id);
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Process(string Name, string Surname, int Age)
        {
            var p = new Person()
            {
                Name = Name,
                Surname = Surname,
                Age = Age
            };

            peopleRepository.context.People.Add(p);
            peopleRepository.context.SaveChanges();

            ViewBag.People = peopleRepository.FindAll();
            return View("Index");
        }

    }
}