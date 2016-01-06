using JetBrains.Annotations;

namespace MVCFeatureBased.Web.Infrastructure.MediatR
{
    public class JSONRedirectResult : IRequestResult
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public object RouteValues { get; set; }

        public JSONRedirectResult([AspMvcController]string controller, [AspMvcAction]string action, object routeValues = null)
        {
            Controller = controller;
            Action = action;
            RouteValues = routeValues;
        }
    }

    public class JSONResult : IRequestResult
    {
        public object Data { get; set; }
    }
}