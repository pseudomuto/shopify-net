using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Net
{
    public partial class ShopifyAPI
    {
        private static readonly string PAGES_ENDPOINT = "admin/pages.json";
        private static readonly string PAGE_ENDPOINT = "admin/pages/{0}.json";

        public Page GetPage(long id)
        {
            var request = new RestRequest(string.Format(PAGE_ENDPOINT, id));
            request.RequestFormat = DataFormat.Json;

            var response = this.ExecuteRequest<PageResponse>(request);
            return response.Data.Page;
        }

        public IEnumerable<Page> GetAllPages()
        {
            var request = new RestRequest(PAGES_ENDPOINT, Method.GET);
            request.RequestFormat = DataFormat.Json;
            
            var response = this.ExecuteRequest<PagesResponse>(request);

            return response.Data.Pages;
        }
    }

    internal class PageResponse
    {
        public Page Page { get; set; }
    }

    internal class PagesResponse
    {
        public List<Page> Pages { get; set; }
    }
}