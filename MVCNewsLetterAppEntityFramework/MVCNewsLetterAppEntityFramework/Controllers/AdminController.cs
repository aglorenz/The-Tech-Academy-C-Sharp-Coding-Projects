using MVCNewsLetterAppEntityFramework.Models;
using MVCNewsLetterAppEntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNewsLetterAppEntityFramework.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (NewsletterEntities db = new NewsletterEntities()) // this instantiation gives us access to the database
            {

                //this is the EntityFramework method

                // as the db grows we will make more specific calls to the database to narrow our list, but for now, we grab all records
                //var signups = db.SignUps;  // represents all the records in the database  This maps to Signups in Newsletter.Context.cs
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList();  // using lambda expression to get the rows still subscribed
                // Here we use another way, LINQ (Language INtegrated Query), to get the rows
                var signups = (from rows in db.SignUps
                               where rows.Removed == null
                               select rows).ToList();
                var signupVms = new List<SignupVm>();  // var is best practice if its obvious what the data type is
                //List<SignupVm> signupVms = new List<SignupVm>();

                // Map our database object to a view model ( so we don't pass personal info to the view) and easier maintenance
                foreach (var signup in signups)
                {
                    var signupVm = new SignupVm();
                    signupVm.Id = signup.Id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    signupVms.Add(signupVm);
                }

                return View(signupVms); // pass the list or records to the view
            }
        }

        public ActionResult Unsubscribe(int Id)
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                var signup = db.SignUps.Find(Id);
                signup.Removed = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}