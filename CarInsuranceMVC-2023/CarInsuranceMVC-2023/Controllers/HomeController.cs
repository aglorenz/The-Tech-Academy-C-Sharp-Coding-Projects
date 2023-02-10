using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVC_2023.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.JumbotronMessage = "We Insure Specialty Cars";
            ViewBag.Button = @"<p class=""text - center""><a href=""/Insuree/Create"" class=""btn btn-secondary"">Click for Free Quote</a></p>";

            return View();
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