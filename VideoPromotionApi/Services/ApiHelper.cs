using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace VideoPromotionApi.Services
{
    public class ApiHelper
    {
        public HttpClient ApiClient { get; set; }
        public ApiHelper(string url)
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(url);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
