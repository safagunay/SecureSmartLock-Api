using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LockerApi.MessageHandlers
{
    public class APIKeyMessageHandler : DelegatingHandler
    {
        private const string _apiKeyToCheck = APIKey.API_KEY;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken)
        {
            bool validKey = false;
            IEnumerable<string> requestHeaders;
            var checkApiKeyExist = message.Headers.TryGetValues("APIKey", out requestHeaders);
            if (checkApiKeyExist)
            {
                if (requestHeaders.FirstOrDefault().Equals(_apiKeyToCheck))
                    validKey = true;

            }
            if (!validKey)
                return message.CreateResponse(HttpStatusCode.Forbidden, "Invalid API Key");

            var response = await base.SendAsync(message, cancellationToken);
            return response;
        }
    }
}