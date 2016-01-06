using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVCFeatureBased.Web.Infrastructure;

namespace MVCFeatureBased.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-DE");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("de-DE");

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
