using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Web.Models;

namespace Task.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(Login login)
        {
            return View(login);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}