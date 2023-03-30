using JobPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace JobPortal.Utility
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var result = new ServiceResult();
                var errors = new List<string>();
                foreach(var state in actionContext.ModelState)
                {
                    foreach(var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage + " " + error.Exception.ToString());
                    }
                }
                string errorStr = string.Join(",", errors.ToArray());
                result.SetFailure(errorStr);

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, result);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
