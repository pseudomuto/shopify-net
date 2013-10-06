using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Should.Fluent;
using Moq;
using RestSharp;
using System.IO;
using RestSharp.Deserializers;

namespace Shopify.Net.Tests.Pages
{
    public class ShopifyAPI
    {
        public class GetPage : Tests.ShopifyAPI
        {
            private IRestRequest _request;
            private Page _subject;

            public GetPage()
            {
                this._apiMock.Setup(m => m.ExecuteRequest<PageResponse>(It.IsAny<IRestRequest>()))
                    .Returns<IRestRequest>((req) =>
                    {
                        this._request = req;
                        return this.MakeResponse<PageResponse>("SampleData/GetPage.json");
                    });

                this._subject = this._api.GetPage(1);
            }

            [Fact]
            public void UsesCorrectEndpoint()
            {
                this._request.Resource
                    .Should().Equal("admin/pages/1.json");
            }

            [Fact]
            public void UsesGetMethod()
            {
                this._request.Method
                    .Should().Equal(Method.GET);
            }

            [Fact]
            public void ParsesId()
            {
                this._subject.Id
                    .Should().Be.GreaterThan(0L);
            }

            [Fact]
            public void ParsesStringValues()
            {
                this._subject.Author.Should().Not.Be.NullOrEmpty();
                this._subject.BodyHTML.Should().Not.Be.NullOrEmpty();
                this._subject.Handle.Should().Not.Be.NullOrEmpty();
                this._subject.Title.Should().Not.Be.NullOrEmpty();
            }

            [Fact]
            public void ParsesDateValues()
            {
                this._subject.CreatedAt
                    .Should().Be.GreaterThan(new DateTime(2013, 01, 01));

                this._subject.PublishedAt
                    .Should().Be.GreaterThan(new DateTime(2013, 01, 01));

                this._subject.UpdatedAt
                    .Should().Be.GreaterThan(new DateTime(2013, 01, 01));
            }
        }

        public class GetAllPages : Tests.ShopifyAPI
        {
            private IEnumerable<Page> _subject;
            private IRestRequest _request;

            public GetAllPages()
            {
                this._apiMock.Setup(m => m.ExecuteRequest<PagesResponse>(It.IsAny<IRestRequest>()))
                    .Returns<IRestRequest>((req) =>
                    {
                        this._request = req;
                        return this.MakeResponse<PagesResponse>("SampleData/GetAllPages.json");
                    });

                this._subject = this._api.GetAllPages();
            }

            [Fact]
            public void CallsTheCorrectEndpoint()
            {
                this._request.Resource
                    .Should().Equal("admin/pages.json");
            }

            [Fact]
            public void UsesGetMethod()
            {
                this._request.Method
                    .Should().Equal(Method.GET);
            }

            [Fact]
            public void ReturnsPageObjects()
            {
                this._subject.Count()
                    .Should().Be.GreaterThan(0);
            }
        }
    }
}
