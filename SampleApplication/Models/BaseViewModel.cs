using Shopify.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApplication.Models
{
    public class BaseViewModel
    {
        public IEnumerable<Page> Pages { get; private set; }

        public BaseViewModel(ShopifyAPI api)
        {
            this.Pages = api.GetAllPages();
        }
    }
}