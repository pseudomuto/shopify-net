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
        public class GetPage : APITest<APIPageResponse>
        {
            private Page _subject;

            public override IRestRequest ExpectedRequest
            {
                get { return new RestRequest("admin/pages/1.json"); }
            }

            public GetPage()
            {
                this._subject = this._api.GetPage(1);
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

        public class GetAllPages : APITest<APIPagesResponse>
        {
            private IEnumerable<Page> _subject;

            public override IRestRequest ExpectedRequest
            {
                get { return new RestRequest("admin/pages.json"); }
            }

            public GetAllPages()
            {
                this._subject = this._api.GetAllPages();
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
