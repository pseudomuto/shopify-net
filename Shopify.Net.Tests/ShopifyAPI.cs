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
        public class Constructor
        {
            [Fact]
            public void RequiresShopName()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new Net.ShopifyAPI(string.Empty, "accessToken");
                });
            }

            [Fact]
            public void RequiresAccessToken()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new Net.ShopifyAPI("storename", string.Empty);
                });
            }
        }

        public class CreatePermanentToken
        {
            private Mock<IRestClient> _client;
            private IRestRequest _request;
            private TempAuthToken _tempToken;
            private string _permanentToken;

            public CreatePermanentToken()
            {
                this._tempToken = new TempAuthToken("clientId", "clientSecret", "tempToken");

                this._client = new Mock<IRestClient>();
                this._client.Setup(m => m.Execute<APIAccessTokenResponse>(It.IsAny<IRestRequest>()))
                    .Returns<IRestRequest>((request) =>
                    {
                        this._request = request;

                        var response = new RestResponse<APIAccessTokenResponse>();
                        response.Data = new APIAccessTokenResponse { AccessToken = "token" };

                        return response;
                    });

                this._permanentToken = Net.ShopifyAPI.CreatePermanentAccessToken(
                        "shop",
                        this._tempToken,
                        this._client.Object
                    );
            }

            [Fact]
            public void ReturnsPermanentToken()
            {
                this._permanentToken
                    .Should().Equal("token");
            }

            [Fact]
            public void RequestsCorrectEndpoint()
            {
                this._request.Resource
                    .Should().Equal("admin/oauth/access_token");
            }

            [Fact]
            public void AddsRequestParameters()
            {
                this._request.Parameters.Count(p => p.Name.Equals("client_id"))
                    .Should().Equal(1);

                this._request.Parameters.Count(p => p.Name.Equals("client_secret"))
                    .Should().Equal(1);

                this._request.Parameters.Count(p => p.Name.Equals("code"))
                    .Should().Equal(1);
            }

            [Fact]
            public void RequiresShopName()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    Net.ShopifyAPI.CreatePermanentAccessToken(
                            string.Empty, 
                            this._tempToken, 
                            this._client.Object
                        );
                });
            }

            [Fact]
            public void RequiresTempAuthToken()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    Net.ShopifyAPI.CreatePermanentAccessToken(
                            string.Empty, 
                            null, 
                            this._client.Object
                        );
                });
            }
        }
    }
}
