using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Services
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var Id = context.HttpContext.Session.GetString("Id");
            var Name = context.HttpContext.Session.GetString("Name");
            var Perms = context.HttpContext.Session.GetString("Perms");
            var companyId = context.HttpContext.Session.GetString("CompanyId");

            if(Id == null && Name == null && Perms == null && companyId == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Account",
                            action = "Login"
                        }));
            }
        }
    }
}
