using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using tecom_test.Validators;

namespace tecom_test
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(NumericValidatorAttribute), typeof(ValidatableObjectAdapter));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
