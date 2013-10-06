using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Net
{
    public partial class ShopifyAPI
    {
        private static readonly string SHOPIFY_URI_FORMAT = "https://{0}.myshopify.com";

        private string _apiKey;
        private string _password;

        public string StoreName { get; private set; }

        public ShopifyAPI(string storeName, string apiKey, string password)
        {
            Guard.AgainstNullOrEmpty("storeName", storeName);
            Guard.AgainstNullOrEmpty("apiKey", apiKey);
            Guard.AgainstNullOrEmpty("password", password);

            this._apiKey = apiKey;
            this._password = password;
            this.StoreName = storeName;
        }

        protected internal virtual IRestResponse<TModel> ExecuteRequest<TModel>(IRestRequest request)
            where TModel : new()
        {
            var client = new RestClient(string.Format(
                    SHOPIFY_URI_FORMAT,
                    this.StoreName
                ));

            client.Authenticator = new HttpBasicAuthenticator(
                    this._apiKey,
                    this._password
                );

            return client.Execute<TModel>(request);
        }
    }
}
