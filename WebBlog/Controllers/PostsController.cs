using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebBlog.Models;

namespace WebBlog.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        
        public ActionResult Index(string postGenero, string searchString)
        {
            var aspNetUsers = _context.AspNetUsers.ToList();

            var GenreLst = new List<string>();

            var GenreQry = from d in _context.Posts
                           orderby d.Genero
                           select d.Genero;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.postGenero = new SelectList(GenreLst);

            var posts = from m in _context.Posts
                          select m;

            //posts = posts.Where(s => s.EmailAutor == Membership.GetUser().Email);
            ////aca va el condicional para chequear el mail del usuario con el del post          

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Titulo.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(postGenero))
            {
                posts = posts.Where(x => x.Genero == postGenero);
            }


            return View(posts);
        }

        [Authorize]
        public ActionResult UserIndex(string postGenero, string searchString)
        {
            var aspNetUsers = _context.AspNetUsers.ToList();

            var GenreLst = new List<string>();

            var GenreQry = from d in _context.Posts
                           orderby d.Genero
                           select d.Genero;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.postGenero = new SelectList(GenreLst);

            var posts = from m in _context.Posts
                        select m;

            //posts = posts.Where(s => s.EmailAutor == Membership.GetUser().Email);
            ////aca va el condicional para chequear el mail del usuario con el del post          

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Titulo.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(postGenero))
            {
                posts = posts.Where(x => x.Genero == postGenero);
            }


            return View(posts);
        }


        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _context.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {           
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Email, Titulo, Genero, Texto")] Post post)
        {
            post.Email = User.Identity.GetUserName();
            post.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("UserIndex");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _context.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(post).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("UserIndex");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _context.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = _context.Posts.Find(id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("UserIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
