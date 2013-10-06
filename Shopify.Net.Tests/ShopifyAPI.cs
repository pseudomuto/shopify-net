using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should.Fluent;
using RestSharp;
using Moq;
using System.IO;
using RestSharp.Deserializers;

namespace Shopify.Net.Tests
{
    public class ShopifyAPI
    {
        protected Mock<Net.ShopifyAPI> _apiMock = new Mock<Net.ShopifyAPI>(
                "shopname", 
                "apiKey", 
                "password"
            );

        protected Net.ShopifyAPI _api;

        public ShopifyAPI()
        {
            this._apiMock.CallBase = true;
            this._api = this._apiMock.Object;
        }

        protected IRestResponse<TModel> MakeResponse<TModel>(string resource)
        {
            var json = File.ReadAllText(resource);
            var serializer = new JsonDeserializer();
            var response = new RestResponse<TModel>();

            response.Content = json;
            response.Data = serializer.Deserialize<TModel>(response);

            return response;
        }

        public class Constructor
        {
            [Fact]
            public void RequiresShopName()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new Net.ShopifyAPI(string.Empty, "apiKey", "pass");
                });
            }

            [Fact]
            public void RequiresAPIKey()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new Net.ShopifyAPI("storename", string.Empty, "password");
                });
            }

            [Fact]
            public void RequiresPassword()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new Net.ShopifyAPI("storename", "apiKey", string.Empty);
                });
            }
        }
    }
}
