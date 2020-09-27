using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VideoPromotionApi.Models;

namespace VideoPromotionApi.Services
{
    public class VideoProcessor
    {
        public string OriginalUrl { get; set; }
        public string Url { get; set; }
        public ApiHelper ApiHelper { get; set; }
        public VideoProcessor(ApiHelper apiHelper, string url)
        {
            ApiHelper = apiHelper;
            OriginalUrl = url;
            Url = url;
        }
        public async Task<VideoModel> LoadVideo()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(Url))
            {
                if (response.IsSuccessStatusCode)
                {
                    VideoModel video = await response.Content.ReadAsAsync<VideoModel>();
                    return video;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
