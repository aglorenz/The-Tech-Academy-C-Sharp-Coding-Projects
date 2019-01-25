using CarInsuranceMVCApp;
using CarInsuranceMVCApp.Models;
using CarInsuranceMVCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Take the fields passed in from the form, and process them to produce the final quote for the customer
        [HttpPost]
        public ActionResult Quote(string firstName, string lastName, string emailAddress, 
                                  DateTime dateOfBirth, int modelYear, string makeOfCar, string modelOfCar,
                                  string dui, int numSpeedingTickets, string coverageType)
        {
            // Start with base of $50/month
            double insQuote = 50.0;

            // calculate age of customer
            DateTime now = DateTime.Now;
            int customerAge = now.Year - dateOfBirth.Year;
            if (dateOfBirth.DayOfYear > now.DayOfYear) customerAge -= 1;
            
            // Increase quote per age risk categories
            if (customerAge < 18) insQuote += 100;
            else if (customerAge < 25) insQuote += 50;
            else if (customerAge > 100) insQuote += 25;

            // Increase quote per age of car
            if (modelYear < 2000) insQuote += 25;
            else if (modelYear > 2015) insQuote += 25;

            // if it's a Porche increase quote, and if it's a Porche 911 Carrera, increase some more
            if (new List<string> { "porche", "porsh", "porshe", "porch", "porsha" }.Contains(makeOfCar.ToLower()))
            {
                insQuote += 25;
                if (new List<string> { "911 carrera", "911 carerra", "911 carera", "911 carrerra" }.Contains(modelOfCar.ToLower()))
                    insQuote += 25;
            }

            // Add 10 for every speeding ticket
            insQuote += (numSpeedingTickets * 10);

            // If DUI, add 25% to total
            if (dui == "Yes") insQuote *= 1.25;

            // If full coverage....
            if (coverageType == "Full") insQuote *= 1.5;

            //round the quote to 2 decimal places before it gets used
            insQuote = Math.Round(insQuote, 2);

            // Save the data to the database
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                var quote = new Quote
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = emailAddress,
                    DateOfBirth = dateOfBirth,
                    ModelOfCar = modelOfCar,
                    MakeOfCar = makeOfCar,
                    ModelYear = modelYear,
                    DUI = (dui == "Yes"), // The form strictly controls what is passed in dui
                    NumSpeedingTickets = numSpeedingTickets,
                    CoverageType = coverageType,
                    MonthlyPremium = insQuote
            };

                db.Quotes.Add(quote);
                db.SaveChanges();
            }

            // Using a ViewModel, prepare the final quote for display to customer.  Capture only relavant info
            var finalQuote = new QuoteVm()
            {
                FirstName = firstName,
                MonthlyPremium = (float)insQuote
            };

            // pass the quote for display to the customer
            return View(finalQuote); // goes to the Quote.cshtml view

        }
    }
}