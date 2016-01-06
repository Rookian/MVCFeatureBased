using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation;
using MediatR;

namespace MVCFeatureBased.Web.Infrastructure.MediatR
{
    public class BaseController : Controller
    {
        private readonly IMediator _mediator;

        public BaseController()
        {
            _mediator = DependencyResolver.Current.GetService<IMediator>();
        }

        protected ActionResult ExecuteRequest<T>(T requestCommand) where T : IRequest<IRequestResult>
        {
            try
            {
                var requestCommandResult = _mediator.Send(requestCommand);
                var actionResult = ActionResult(requestCommandResult);
                var expression = new RequestCommandResultExpression(actionResult);

                return expression;
            }
            catch (ValidationException exception)
            {
                var expression = new RequestCommandResultExpression(exception);
                return expression;
            }
        }

        protected async Task<RequestCommandResultExpression> ExecuteRequestAsync<T>(T requestCommand) where T : IAsyncRequest<IRequestResult>
        {
            try
            {
                var requestCommandResult = await _mediator.SendAsync(requestCommand);
                var actionResult = ActionResult(requestCommandResult);
                var expression = new RequestCommandResultExpression(actionResult);

                return expression;
            }
            catch (ValidationException exception)
            {
                //ModelState.AddModelErrors(exception);
                var expression = new RequestCommandResultExpression(exception);
                return expression;
            }
        }

        private ActionResult ActionResult(IRequestResult result)
        {
            if (result == null)
            {
                return null;
            }

            var anchor = result as RedirectToActionResultWithAnchor;
            if (anchor != null)
            {
                return Redirect(Url.RouteUrl(new { anchor.Controller, anchor.Action }) + GetQueryParams(anchor.RouteValues) + "#" + anchor.Anchor);
            }

            var redirect = result as RedirectToActionResult;
            if (redirect != null)
            {
                return RedirectToAction(redirect.Action, redirect.Controller, redirect.RouteValues);
            }

            var emptyResult = result as EmptyActionResult;
            if (emptyResult != null)
            {
               
                return new EmptyResult();
            }

            var view = result as ViewResult;
            if (view != null)
            {
                return View(view.View, view.Model);
            }

            //var jsonRedirect = result as JSONRedirectResult;
            //if (jsonRedirect != null)
            //{
            //    var redirectUrl = Url.Action(jsonRedirect.Action, jsonRedirect.Controller, jsonRedirect.RouteValues);
            //    return Json(new ValidationSuccessResult { RedirectUrl = redirectUrl }, JsonRequestBehavior.AllowGet);
            //}

            var jsonResult = result as JSONResult;
            if (jsonResult != null)
            {
                return Json(jsonResult.Data, JsonRequestBehavior.AllowGet);
            }

            var fileResult = result as FileResult;
            if (fileResult != null)
            {
                return File(fileResult.Data, fileResult.ContentType, fileResult.FileName);
            }

            throw new ArgumentException($"Konnte IRequestCommandResultBase ({result.GetType()}) nicht in ActionResult umwandeln");
        }

      
        private string GetQueryParams(RouteValueDictionary routeValues)
        {
            if (routeValues == null)
                return "";
            if (routeValues.Count == 0)
                return "";
            return "?" + string.Join("&", routeValues.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value.ToString())}").ToArray());
        }
    }
}