using CarInsuranceMVCApp.Models;
using CarInsuranceMVCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVCApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                var quotes = db.Quotes; // this var represents all rows in the Quotes table
                var quoteVms = new List<QuoteVm>();
                foreach (var quote in quotes)
                {
                    var quoteVm = new QuoteVm
                    {
                        // map the applicable columns from the Model to our ViewModel
                        FirstName = quote.FirstName,
                        LastName = quote.LastName,
                        EmailAddress = quote.EmailAddress,
                        MonthlyPremium = (float)quote.MonthlyPremium
                    };
                    quoteVms.Add(quoteVm); // add the row to our list of quotes
                }
                return View(quoteVms);  // and display to our view
            }
        }
    }
}