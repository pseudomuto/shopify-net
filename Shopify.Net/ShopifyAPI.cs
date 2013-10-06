using RestSharp;
using RestSharp.Deserializers;
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
        private static readonly string OAUTH_HEADER_NAME = "X-Shopify-Access-Token";

        private string _accessToken;

        public string StoreName { get; private set; }

        public ShopifyAPI(string storeName, string accessToken)
        {
            Guard.AgainstNullOrEmpty("storeName", storeName);
            Guard.AgainstNullOrEmpty("accessToken", accessToken);

            this._accessToken = accessToken;
            this.StoreName = storeName;
        }

        public static string CreatePermanentAccessToken(string shopName, TempAuthToken tempToken, IRestClient client = null)
        {
            Guard.AgainstNullOrEmpty("shopName", shopName);
            Guard.AgainstNull("tempToken", tempToken);

            var request = new RestRequest("admin/oauth/access_token", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("client_id", tempToken.Code);
            request.AddParameter("client_secret", tempToken.ClientSecret);
            request.AddParameter("code", tempToken.Code);

            client = client ?? new RestClient(string.Format(
                    "https://{0}.myshopify.com", 
                    shopName
                ));

            var response = client.Execute<APIAccessTokenResponse>(request);
            return response.Data.AccessToken;
        }

        protected internal virtual IRestResponse<TModel> ExecuteRequest<TModel>(IRestRequest request) 
            where TModel : new()
        {
            var client = new RestClient(string.Format(
                    SHOPIFY_URI_FORMAT,
                    this.StoreName
                ));

            request.AddHeader(OAUTH_HEADER_NAME, this._accessToken);

            return client.Execute<TModel>(request);
        }
    }

    public class APIAccessTokenResponse
    {
        public string AccessToken { get; set; }
    }
}
