using MediatR;
using MVCFeatureBased.Web.Infrastructure.MediatR;

namespace MVCFeatureBased.Web.Features.Home
{
    public class RequestQuery : IRequest<IRequestResult> {}

    public class ViewModel
    {
        public string Greeting { get; set; }
    }

    public class RequestQueryHandler : IRequestHandler<RequestQuery, IRequestResult>
    {
        public IRequestResult Handle(RequestQuery message)
        {
            // Index is marked as red by R#
            return new ViewResult("Index", new ViewModel {Greeting = "Hello World!"});
        }
    }
}