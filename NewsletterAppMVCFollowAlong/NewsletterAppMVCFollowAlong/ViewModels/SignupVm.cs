using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/* 
 * Viewmodels are considered a best practice in almost all cases where your model is what maps exactly to the db
 * and you pull in fully populated models.  You don't just pass all that information to the view, you create a 
 * viewmodel.  You transfer the information you need from the model to the viewmodel, then you pass the viewmodel 
 * to the view.  This is considered a best practice.
 */
namespace NewsletterAppMVCFollowAlong.ViewModels
{
    public class SignupVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}