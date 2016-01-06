using System.Web.Routing;
using JetBrains.Annotations;

namespace MVCFeatureBased.Web.Infrastructure.MediatR
{
    public class RedirectToActionResult : IRequestResult
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public RouteValueDictionary RouteValues { get; set; }

        public RedirectToActionResult([AspMvcController]string controller, [AspMvcAction]string action, RouteValueDictionary routeValueDictionary = null)
        {
            Action = action;
            Controller = controller;
            RouteValues = routeValueDictionary;
        }
    }
}