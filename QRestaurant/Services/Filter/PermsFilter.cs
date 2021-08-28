using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Services
{
    public class PermsFilter : ActionFilterAttribute
    {
        public int Role { get; set; }
        public override void OnActionExecuting(ActionExecutingContext Context)
        {
            var perms = Context.HttpContext.Session.GetString("Perms").Split(',');
            if(perms[Role] != "1")
            {
                Context.Result = new UnauthorizedResult();
            }
        }
    }
}
