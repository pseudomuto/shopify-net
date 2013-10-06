using SampleApplication.Models;
using Shopify.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleApplication.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            var api = APIFactory.Create();
            var model = new BaseViewModel(api);

            return this.View(model);
        }

        [HttpGet]
        public ActionResult SetupRequired()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult SetupRequired(string apiKey, string secretKey)
        {
            var returnURL = string.Format(
                    "http://{0}/home/auth",
                    this.Request.Url.Authority
                );

            var cookie = new HttpCookie("tempAuth");
            cookie.Values.Add("key", apiKey);
            cookie.Values.Add("secret", secretKey);

            var url = APIFactory.AuthorizeURL(apiKey, returnURL);
            this.Response.Cookies.Add(cookie);
            return this.Redirect(url);
        }
                
        public ActionResult Auth()
        {
            var cookie = this.Request.Cookies["tempAuth"];
            
            var tempCode = this.Request.QueryString["code"];
            APIFactory.MakePermanentAuthToken(
                    cookie.Values["key"],
                    cookie.Values["secret"],
                    tempCode
                );

            this.Response.Cookies.Remove("tempAuth");
            return this.RedirectToAction("Index");
        }
    }
}
