using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsuranceMVCApp.ViewModels
{
    public class QuoteVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public float MonthlyPremium { get; set; }
    }
}