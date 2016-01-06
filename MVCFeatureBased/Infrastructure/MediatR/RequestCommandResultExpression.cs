using System;
using System.Web.Mvc;
using FluentValidation;

namespace MVCFeatureBased.Web.Infrastructure.MediatR
{
    public class RequestCommandResultExpression
    {
        private readonly ValidationException _validationException;
        private ActionResult _validationErrorResult;
        private readonly ActionResult _successResult;
        private Func<ValidationException, ActionResult> _onValidationErrorWithActionResult;

        public RequestCommandResultExpression(ActionResult actionResult)
        {
            _successResult = actionResult;
        }

        public RequestCommandResultExpression(ValidationException validationException)
        {
            _validationException = validationException;
        }

        public RequestCommandResultExpression OnValidationError(Func<ValidationException, ActionResult> onValidationError)
        {
            _onValidationErrorWithActionResult = onValidationError;
            return this;
        }

        public static implicit operator ActionResult(RequestCommandResultExpression expression)
        {
            if (expression._onValidationErrorWithActionResult != null)
            {
                expression._validationErrorResult = expression._onValidationErrorWithActionResult(expression._validationException);
            }

            return expression._successResult ?? expression._validationErrorResult;
        }

    }
}