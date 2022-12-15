using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToDataverse.services
{
    internal  class DataverseClient
    {
        HttpClient _client;
        public HttpClient Client { get => _client; set => _client = value; }
        public DataverseClient(string resource,string token)
        {
            Client = new HttpClient
            {
                // See https://docs.microsoft.com/powerapps/developer/data-platform/webapi/compose-http-requests-handle-errors#web-api-url-and-versions
                BaseAddress = new Uri(resource + "/api/data/v9.2/"),
                Timeout = new TimeSpan(0, 2, 0)    // Standard two minute timeout on web service calls.
            };


            //https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/about?view=dataverse-latest
            // Default headers for each Web API call.
            // See https://docs.microsoft.com/powerapps/developer/data-platform/webapi/compose-http-requests-handle-errors#http-headers
            HttpRequestHeaders headers = Client.DefaultRequestHeaders;
            headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            headers.Add("OData-MaxVersion", "4.0");
            headers.Add("OData-Version", "4.0");
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        

        //public async Task<string> Get(string url)
        //{
            
        //    var response = await Client.GetAsync(url);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}
        //public async Task<HttpResponseMessage> GetAsync(string url)
        //{
        //    var response = await Client.GetAsync(url);            
        //    return response;
        //}

    }
}
