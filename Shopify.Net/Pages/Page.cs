using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Net
{
    public class Page
    {
        public long Id { get; set; }

        public long ShopId { get; set; }

        public string Title { get; set; }

        public string Handle { get; set; }

        public string Author { get; set; }

        public string BodyHTML { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime PublishedAt { get; set; }
    }
}
