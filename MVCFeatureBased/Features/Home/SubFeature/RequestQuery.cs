using MediatR;
using MVCFeatureBased.Web.Infrastructure.MediatR;

namespace MVCFeatureBased.Web.Features.Home.SubFeature
{
    public class RequestQuery : IRequest<IRequestResult> { }

    public class ViewModel
    {
        public string SubFeature { get; set; }
    }

    public class RequestQueryHandler : IRequestHandler<RequestQuery, IRequestResult>
    {
        public IRequestResult Handle(RequestQuery message)
        {
            // SubFeature/SubFeature is marked as red by R#
            return new ViewResult("SubFeature/SubFeature", new ViewModel { SubFeature = "SubFeature!" });
        }
    }
}