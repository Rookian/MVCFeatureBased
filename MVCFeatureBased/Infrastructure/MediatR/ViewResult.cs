using JetBrains.Annotations;

namespace MVCFeatureBased.Web.Infrastructure.MediatR
{
    public class ViewResult : IRequestResult
    {
        public ViewResult([AspMvcView]string view, object model)
        {
            View = view;
            Model = model;
        }

        public string View { get; private set; }
        public object Model { get; private set; }
    }
}