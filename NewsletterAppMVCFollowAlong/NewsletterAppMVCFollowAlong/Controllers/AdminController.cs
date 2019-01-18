using NewsletterAppMVCFollowAlong.Models;
using NewsletterAppMVCFollowAlong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsletterAppMVCFollowAlong.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                // SignUps represents all the records in our SignUps table.  This one line of code, is
                // equivalent to the 15 or so lines of commented code below, i.e., query string, connection
                // and mappings from reader[].
                
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList(); // using lambda expression
                // or using the linq query
                var signups = (from c in db.SignUps
                               where c.Removed == null
                               select c).ToList();

                var signupVms = new List<SignupVm>(); //  SignUp table ViewModel

                // map our db object to a viewmodel so that we don't pass private or personal 
                // info (ssn) to the view
                foreach (var signup in signups)
                {
                    var signupVm = new SignupVm();
                    signupVm.Id = signup.id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    signupVms.Add(signupVm);
                }

                return View(signupVms);
            }
            //string queryString = @"SELECT Id, FirstName, LastName, EmailAddress, SocialSecurityNumber FROM Signups";
            //List<Signup> signups = new List<Signup>();

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);

            //    connection.Open();

            //    SqlDataReader reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        var signup = new Signup();
            //        signup.Id = Convert.ToInt32(reader["Id"]);
            //        signup.FirstName = reader["FirstName"].ToString();
            //        signup.LastName = reader["LastName"].ToString();
            //        signup.EmailAddress = reader["EmailAddress"].ToString();
            //        signup.SocialSecurityNumber = reader["SocialSecurityNumber"].ToString();

            //        signups.Add(signup);
            //    }
            //}
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