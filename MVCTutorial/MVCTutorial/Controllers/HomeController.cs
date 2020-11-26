using MVCTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTutorial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string text = "Hello";
            //System.IO.File.WriteAllText(@"C:/users/andy/samplelog.txt", text);

            //    Random rnd = new Random(10);
            //    var num = rnd.Next();

            //    if (num > 20000)
            //    {
            //        return View("About");
            //    }

            //return View();  // returns the index view.  Default is to look for the view associated with the ActionResult
            //return View("Index"); // returns the index view

            //return Content("Hello");  // Don't alwyas have to return a view.  returns "Hello" in the view without the layout.cshtml
            //return Content(""); // Can also return nothing

            //return RedirectToAction("Contact");  // sends control to the Contact() action (method).  url then says localhost:44325/Home/Contact
            //return View("Contact");  //  different than above because the url only says localhost:44325 AND the ViewBag.Message does
            // not get displayed

            //return View();          // url says localhost:44325
            //return View("Index");   // url says localhost:44325 

            //----------------------------------
            //int number = 5;
            //return View(number);

            //----------------------------------
            //List<string> names = new List<string>
            //{
            //    "Andy",
            //    "Jesse",
            //    "Mary"
            //};
            //return View(names);

            User user = new User();
            user.Id = 1;
            user.FirstName = "Jesse";
            user.LastName = "Johnson";
            user.Age = 34;
            return View(user);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //throw new Exception("Invalid page");  // you get "Server error in '/' Application

            return View();
            //return View("Index");
            //return RedirectToAction("Index");
        }

        public ActionResult Contact(int id=0) // can also accept a parm. Must use int id, not int number. id is req'd (it's a builtin MVC configuration)
                                            // and you pass the parameter in on the url like so:  localhost:44325/home/contact/5 where id=5
        {
            ViewBag.Message = id;

            return View();
        }
        
        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Andy, This is your contact page.";

        //    return View();
        //}
    }
}