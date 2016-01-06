using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using JetBrains.Annotations;

namespace MVCFeatureBased.Web.Infrastructure.MVC
{
    public interface IUrlHelper
    {
        string GetUrlFor([AspMvcController] string controller, [AspMvcAction] string action, Dictionary<string, object> routeValueDictionary = null);
        string GetUrlFor([AspMvcController] string controller, [AspMvcAction] string action, object obj);
    }

    public class UrlHelper : IUrlHelper
    {
        public string GetUrlFor([AspMvcController] string controller, [AspMvcAction] string action, Dictionary<string, object> parameter = null)
        {
            var rvd = parameter == null ? new RouteValueDictionary() : new RouteValueDictionary(parameter);
            return new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext).Action(action, controller, rvd);
        }

        public string GetUrlFor([AspMvcController] string controller, [AspMvcAction] string action, object obj)
        {
            return new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext).Action(action, controller, obj);
        }
    }
}