using WebBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult About()
        {
            ViewBag.Message = "My Blogger";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Podes ver mi repositorio de github";

            return View();
        }

        public FileResult Download()
        {            
            var myPath = Server.MapPath("~/CV Javier FM.docx");
            return File(myPath, "application/pdf", "CV_JavierFM.docx");
        }

        
    }
}