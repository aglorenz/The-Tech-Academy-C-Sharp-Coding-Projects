﻿using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVC_2023
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
