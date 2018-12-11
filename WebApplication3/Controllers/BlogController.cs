using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Misc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class BlogController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Blog
        public ActionResult Index(int from=0)
        {
            if (from < 0)
                throw new HttpException(400, "Invalid Request");
            ViewBag.bp = db.BlogPosts.OrderByDescending(r => r.DateCreated).Skip(from).Take(2).ToList();
            ViewBag.from = from;
            ViewBag.bpRecent = db.BlogPosts.OrderByDescending(r => r.DateCreated).FirstOrDefault();
            return View();
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            BlogPost blogPost = null;
            if (id < 0)
            {
                blogPost = db.BlogPosts.FirstOrDefault();
            }
            else
            {
                blogPost = db.BlogPosts.Find(id);
            }
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.bp = blogPost;
            return View();
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Title,Content")] BlogPost blogPost)
        {
            blogPost.IDAuthor = new SessionManager(this).LoggedInUser.ID;
            blogPost.DateCreated = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.BlogPosts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Title,Content")]BlogPost bp)
        {
            if (bp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bpInDB = db.BlogPosts.Find(bp.ID);
            if(bpInDB == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //bpInDB.Picture = bp.Picture;
            bpInDB.Title = bp.Title;
            bpInDB.Content = bp.Content;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = bp.ID });
            //return View("Details","Blog",new { id = bp.ID});
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if(blogPost == null)
            {
                return RedirectToAction("index");
            }

            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
