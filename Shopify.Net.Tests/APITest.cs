using Moq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should.Fluent;

namespace Shopify.Net.Tests
{
    public abstract class APITest<TResponseModel> where TResponseModel : new()
    {
        protected Mock<Net.ShopifyAPI> _apiMock = new Mock<Net.ShopifyAPI>(
                "shopname",
                "accessToken"
            );

        protected Net.ShopifyAPI _api;
        protected IRestRequest _request;

        public abstract IRestRequest ExpectedRequest { get; }

        protected APITest()
        {
            this._apiMock.CallBase = true;
            this._apiMock.Setup(m => m.ExecuteRequest<TResponseModel>(It.IsAny<IRestRequest>()))
                    .Returns<IRestRequest>((req) =>
                    {
                        this._request = req;

                        var name = this.GetType().Name;
                        return this.MakeResponse<TResponseModel>(string.Format(
                                "SampleData/{0}.json",
                                name
                            ));
                    });

            this._api = this._apiMock.Object;
        }

        [Fact]
        public void RequestsCorrectEndpoint()
        {
            this._request.Resource
                .Should().Equal(this.ExpectedRequest.Resource);
        }

        [Fact]
        public void RequestsUsingCorrectMethod()
        {
            this._request.Method
                .Should().Equal(this.ExpectedRequest.Method);
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
    }
}
