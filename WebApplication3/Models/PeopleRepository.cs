using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class PeopleRepository
    {
        public MyContext context = new MyContext();

        public List<Person> FindAll()
        {
            return context.People.ToList();
        }

        public Person FindById(int id)
        {
            return context.People.Find(id);
        }
        
        public void Create(Person p)
        {
            context.People.Add(p);
            context.SaveChanges();
        }

        public void Update(Person p)
        {
            var dbo = FindById(p.Id);

            dbo.Age = p.Age;
            dbo.Id = p.Id;
            dbo.Name = p.Name;
            dbo.Surname = p.Surname;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbo = FindById(id);

            context.People.Remove(dbo);
            context.SaveChanges();
        }

    }
}