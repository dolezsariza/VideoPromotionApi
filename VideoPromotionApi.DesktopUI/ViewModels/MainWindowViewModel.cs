using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VideoPromotionApi.Models;
using VideoPromotionApi.Services;

namespace VideoPromotionApi.DesktopUI.ViewModels
{
    public class MainWindowViewModel
    {
        public string UserName { get; set; }
        public string API_KEY { get; set; }
        public string IPAddress { get; set; }
        public List<Display> DataToShow { get; set; }
        public ApiHelper ApiHelper { get; set; }
        public VideoProcessor VideoProcessor { get; set; }
        public IPGetter IPGetter { get; set; }
        public FileHandler FileHandler { get; set; }
        private string[] textData;
        private string baseUrl;

        public MainWindowViewModel()
        {
            baseUrl = "https://pt.potawe.com/api/video-promotion/v1/list";
            ApiHelper = new ApiHelper(baseUrl);
            FileHandler = new FileHandler();
            IPGetter = new IPGetter();
            DataToShow = new List<Display>();
            var url = GetUrl();
            VideoProcessor = new VideoProcessor(ApiHelper, url);
        }
        public async Task LoadVideo()
        {
            var videoModel = await VideoProcessor.LoadVideo();
            foreach (var video in videoModel.Data.Videos)
            {
                var thumbnailLink = $"https:{video.PreviewImages[0]}";
                var uriSource = new Uri(thumbnailLink, UriKind.Absolute);
                var data = new Display(new BitmapImage(uriSource),
                    video.Title,
                    video.Duration,
                    video.Quality,
                    video.Uploader,
                    video.Tags);
                data.Tags.Sort();
                DataToShow.Add(data);
            }
        }

        private string GetUrl()
        {
            textData = FileHandler.ReadFromFile();
            UserName = textData[0];
            API_KEY = textData[1];
            IPAddress = IPGetter.GetIpOfHost();
            var url = $"{baseUrl}?psid={UserName}&accessKey={API_KEY}&clientIp={IPAddress}";
            return url;
        }
    }
}
