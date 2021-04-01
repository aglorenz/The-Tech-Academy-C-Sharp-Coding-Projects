using MVCNewsLetterAppEntityFramework.Models;
using MVCNewsLetterAppEntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNewsLetterAppEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        // not used now that we are using Entity Framework.  We are using NewsletterEntities as the connex str.  Its in Web.config now
        // and was automatically added when we imported the data model ADO.NET Entity Data Model
        //private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Newsletter;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        /// <summary>
        ///  Displays the view housing the signup form
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// After user clicks submit, take the data from the signup form and post it to the database.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        /// <returns>Success View</returns>
        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                //return null; // do this when unit testing
                return View("~/Views/Shared/Error.cshtml"); // ~ means relative at the root
            }
            else
            {
                //Using Entity Framework is much easier to access the database than ADO.NET
                using (NewsletterEntities db = new NewsletterEntities())
                {
                    var signup = new SignUp();
                    signup.FirstName = firstName;
                    signup.LastName = lastName;
                    signup.EmailAddress = emailAddress;

                    db.SignUps.Add(signup);
                    db.SaveChanges();
                }
                //Now we will connect to our database with ADO.net
                //Using parameters helps prevent SQL injection
                //string queryString = @"INSERT INTO SignUps (FirstName, LastName, EmailAddress) 
                //                    VALUES
                //                    (@FirstName, @LastName, @EmailAddress)";
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
                //    command.ExecuteNonQuery();  // Its an insert so technically, it's a non-query
                //    connection.Close();
                //}
                return View("Success");
            }
        }



                // Below is the old ADO.NET method.  Notice its longer than the 
                //string queryString = @"SELECT Id, FirstName, LastName, EmailAddress, SocialSecurityNumber FROM SignUps";
                //List<NewsletterSignUp> signups = new List<NewsletterSignUp>();

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    SqlCommand command = new SqlCommand(queryString, connection);

                //    connection.Open();

                //    SqlDataReader reader = command.ExecuteReader();

                //    while (reader.Read())
                //    {
                //        var signup = new NewsletterSignUp();
                //        signup.Id = Convert.ToInt32(reader["Id"]);
                //        signup.FirstName = reader["FirstName"].ToString();
                //        signup.LastName = reader["LastName"].ToString();
                //        signup.EmailAddress = reader["EmailAddress"].ToString();
                //        signup.SocialSecurityNumber = reader["SocialSecurityNumber"].ToString(); // wwe don't want to return this

                //        signups.Add(signup);
                //    }
                //}

                //-----------------------------------------------------------------------------------
                //--------------- Above part is the ADO.NET extra code needed above what EF needs -------------------------
                //-----------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------
                // --------------- Below  we map our database object to a viewModel
                //-----------------------------------------------------------------------------------

                //    var signupVms = new List<SignupVm>();  // var is best practice if its obvious what the data type is
                //    //List<SignupVm> signupVms = new List<SignupVm>();

                //    foreach (var signup in signups)
                //    {
                //        var signupVm = new SignupVm();
                //        signupVm.FirstName = signup.FirstName;
                //        signupVm.LastName = signup.LastName;
                //        signupVm.EmailAddress = signup.EmailAddress;
                //        signupVms.Add(signupVm);
                //    }

                //    return View(signupVms);
                //}

    }
}