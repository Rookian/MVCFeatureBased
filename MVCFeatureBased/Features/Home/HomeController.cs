using System.Web.Mvc;
using MVCFeatureBased.Web.Infrastructure.MediatR;

namespace MVCFeatureBased.Web.Features.Home
{
    public class HomeController : BaseController
    {
        public ActionResult Index(RequestQuery query) => ExecuteRequest(query);
        public ActionResult SubFeature(SubFeature.RequestQuery query) => ExecuteRequest(query);
    }
}