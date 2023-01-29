using CarInsuranceMVC_2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsuranceMVC_2023.Services
{
    public class QuoteServices
    {
        /// <summary>
        /// Calculate the insurance quote for the given insuree.
        /// </summary>
        /// <param name="insuree">Insuree object containing fields from input form</param>
        /// <returns>Insurance quote</returns>
        public static decimal CalculateQuote(Insuree insuree)
        {
            decimal quote = 50M; // start with base quote of 50.00

            // Determine age premium
            int age = GetInsureeAge(insuree.DateOfBirth);
            if (age <= 18)
                quote += 100;  // add $100 if 18 years or under
            else if (age > 18 && age <= 25)
                quote += 50;   // add $50 if between 19 and 25 years old
            else quote += 25;  // add $25 if over 25 years old

            // if car year is before 2000 or over 2015, add $25
            if ((insuree.CarYear > 2000) || (insuree.CarYear > 2015))
                quote += 25;

            // if car make is Porsche, add $25.  If model is 911 Carrera add additional $25
            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25;
                if (insuree.CarModel.ToLower() == "911 carrera")
                    quote += 25;
            }

            // Add $10 for each speeding ticket
            if (insuree.SpeedingTickets > 0)
                quote += insuree.SpeedingTickets * 10;

            // Add 25% to toal if insuree has DUI
            if (insuree.DUI) quote *= 1.25M;

            // Add 50% for full coverage
            if (insuree.CoverageType) quote *= 1.5M;

            return quote;
        }

        /// <summary>
        /// Calculate age given a birthdate
        /// </summary>
        /// <param name="birthDate">Birthdate of insuree applicant</param>
        /// <returns>Age in whole years</returns>
        private static int GetInsureeAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year; // get age in years

            // refine age depending if birthday has occurred this year.
            int curMonth = today.Month;
            int curDay = today.Day;
            int birthMonth = birthDate.Month;
            int birthDay = birthDate.Day;
            // Subtract 1 if birthday has not occurred yet
            if ((birthMonth > curMonth) || (birthMonth == curMonth && birthDay > curDay))
                age--;

            return age;
        }
    }
}
