using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Helpers
{

    public class ValidatorActionFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new BadRequestObjectResult(new GenericResponse<object> { Data = null, Message = string.Join(Environment.NewLine, GetErrorListFromModelState(filterContext.ModelState)) });
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
        public static List<string> GetErrorListFromModelState
                                              (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
    }

    public sealed class PresentOrFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (Convert.ToDateTime(value) >= DateTime.Today)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Past date not allowed.");
                }
            }
            catch
            {
                return new ValidationResult("Invalid Date");
            }
        }
    }
}
