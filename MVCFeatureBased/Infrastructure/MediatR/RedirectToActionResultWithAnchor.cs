using System.Web.Routing;
using JetBrains.Annotations;

namespace MVCFeatureBased.Web.Infrastructure.MediatR
{
    public class RedirectToActionResultWithAnchor : IRequestResult
    {
        public string Action { get; private set; }
        public string Controller { get; private set; }
        public RouteValueDictionary RouteValues { get; private set; }
        public string Anchor { get; private set; }

        public RedirectToActionResultWithAnchor([AspMvcController]string controller, [AspMvcAction]string action, string anchor, RouteValueDictionary routeValueDictionary = null)
        {
            Action = action;
            Anchor = anchor;
            Controller = controller;
            RouteValues = routeValueDictionary;
        }
    }
}