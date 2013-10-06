using SampleApplication.Models;
using Shopify.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleApplication.Controllers
{
    public class ApplicationController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!this.IsSetupAction(filterContext))
            {
                if (APIFactory.RequiresSetup())
                {
                    filterContext.Result = this.RedirectToAction("SetupRequired", "Home");
                }
            }
        }

        private bool IsSetupAction(ActionExecutingContext filterContext)
        {
            var action = filterContext.RouteData.GetRequiredString("action");
            return action.Equals("SetupRequired", StringComparison.OrdinalIgnoreCase);
        }
    }
}
