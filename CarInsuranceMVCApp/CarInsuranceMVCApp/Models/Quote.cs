//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarInsuranceMVCApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Quote
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string MakeOfCar { get; set; }
        public string ModelOfCar { get; set; }
        public int ModelYear { get; set; }
        public bool DUI { get; set; }
        public int NumSpeedingTickets { get; set; }
        public string CoverageType { get; set; }
        public double MonthlyPremium { get; set; }
    }
}
