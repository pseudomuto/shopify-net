using SampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (APIFactory.RequiresSetup())
            {
                return this.RedirectToAction("SetupRequired");
            }

            var api = APIFactory.Create();
            return this.View(api);
        }

        public ActionResult SetupRequired()
        {
            return this.View();
        }
    }
}
