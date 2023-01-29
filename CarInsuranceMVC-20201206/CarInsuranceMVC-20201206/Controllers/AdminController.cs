using CarInsuranceMVC_20201206.Models;
using CarInsuranceMVC_20201206.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVC_20201206.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            List<InsureeVM> insureeVMs = new List<InsureeVM>();
            using (InsuranceEntities db = new InsuranceEntities())
            {
                var insurees = db.Insurees;
                foreach (var insuree in insurees)
                {
                    var insureeVM = new InsureeVM();
                    insureeVM.FirstName = insuree.FirstName;
                    insureeVM.LastName = insuree.LastName;
                    insureeVM.EmailAddress = insuree.EmailAddress;
                    insureeVM.Quote = insuree.Quote;
                    insureeVMs.Add(insureeVM);
                }
            }
            return View(insureeVMs);
        }
    }
}