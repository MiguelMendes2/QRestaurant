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
        public int Role0 { get; set; }

        public override void OnActionExecuting(ActionExecutingContext Context)
        {
            var perms = Convert.ToInt32(Context.HttpContext.Session.GetString("Perms"));

            if(Role0 != perms || Role0 > perms)
            {
                Context.Result = new UnauthorizedResult();
            }

        }
    }
}
