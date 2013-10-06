using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Net
{
    public class TempAuthToken
    {
        public string ClientId { get; private set; }

        public string ClientSecret { get; private set; }

        public string Code { get; private set; }

        public TempAuthToken(string clientId, string clientSecret, string tempCode)
        {
            Guard.AgainstNullOrEmpty("clientId", clientId);
            Guard.AgainstNullOrEmpty("clientSecret", clientSecret);
            Guard.AgainstNullOrEmpty("tempCode", tempCode);

            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.Code = tempCode;
        }
    }
}
