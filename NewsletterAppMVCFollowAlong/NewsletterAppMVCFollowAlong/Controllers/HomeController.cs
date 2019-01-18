using NewsletterAppMVCFollowAlong.Models;
using NewsletterAppMVCFollowAlong.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsletterAppMVCFollowAlong.Controllers
{
    public class HomeController : Controller
    {
        //private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Newsletter;" +
        //                           "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
        //                           "TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
        //                           "MultiSubnetFailover=False";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(string firstName, string lastName, string emailAddress)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/error.cshtml");
            }
            else
            {
                using (NewsletterEntities db = new NewsletterEntities())
                {
                    var signup = new SignUp();
                    signup.FirstName = firstName;
                    signup.LastName = lastName;
                    signup.EmailAddress = emailAddress;

                    db.SignUps.Add(signup);
                    db.SaveChanges(); // nothing is saved to the database until we execute this statement
                }
                // All the code below is replaced by the using statement above using entity framework (love!)
                // Entity Framework is a wrapper for ADO.net.  The interface it provides up is mui simple.
                //string queryString = @"INSERT INTO SignUps (FirstName, LastName, EmailAddress)
                //                       VALUES (@FirstName, @LastName, @EmailAddress)";

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    SqlCommand command = new SqlCommand(queryString, connection);
                //    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                //    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                //    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);

                //    command.Parameters["@FirstName"].Value = firstName;
                //    command.Parameters["@LastName"].Value = lastName;
                //    command.Parameters["@EmailAddress"].Value = emailAddress;

                //    connection.Open();
                //    command.ExecuteNonQuery();
                //    connection.Close();
                //}

                return View("Success");
            }
        }

        //public ActionResult Admin()
        //{
            
            
        //}
    }
}