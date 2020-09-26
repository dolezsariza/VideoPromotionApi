using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Display> DataToShow { get; set; }
        public Pagination Pagination { get; set; }
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
            DataToShow = new ObservableCollection<Display>();
            var url = GetUrl();
            VideoProcessor = new VideoProcessor(ApiHelper, url);
        }

        public async Task LoadVideo()
        {
            DataToShow.Clear();
            try
            {
                var videoModel = await VideoProcessor.LoadVideo();
            
                Pagination = videoModel.Data.Pagination;

                foreach (var video in videoModel.Data.Videos)
                {
                    var thumbnailLink = $"https:{video.PreviewImages[0]}";
                    var uriSource = new Uri(thumbnailLink, UriKind.Absolute);
                    var data = new Display(new BitmapImage(uriSource),
                        video.Title,
                        video.Duration,
                        video.Quality.ToUpper(),
                        video.Uploader,
                        video.Tags);
                    data.Tags.Sort();
                    DataToShow.Add(data);
                }
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                var uriSource = new Uri("https://www.iconfinder.com/data/icons/image-1/64/Image-12-512.png", UriKind.Absolute);
                var substitute = new Display(new BitmapImage(uriSource),
                    "No videos found, please check, if you're connected to the internet and provided the correct PSID and access key in Assets\\API.txt!",
                    0, "", "", null);
                Pagination = new Pagination()
                {
                    CurrentPage = 1,
                    TotalPages = 0
                };
                DataToShow.Add(substitute);
            }
        }

        public async Task FilterVideo(string quality, string uploader)
        {
            var newUrl = VideoProcessor.OriginalUrl;
            newUrl += !quality.Equals("All") ? $"&quality={quality}" : "";
            newUrl += !uploader.Equals("All") ? $"&uploader={uploader}" : "";

            VideoProcessor.Url = newUrl;
            await LoadVideo();

        }

        public async Task ChangePage(string page)
        {
            var pageUrl = VideoProcessor.Url;
            var currentPage = Pagination.CurrentPage;
            if (pageUrl.Contains("pageIndex"))
            {
                int index = pageUrl.LastIndexOf("&");
                pageUrl = pageUrl.Substring(0, index);
            }
            switch (page)
            {
                case "first":
                    currentPage = 1;
                    break;
                case "previous":
                    if(currentPage > 1)
                        currentPage--;
                    break;
                case "next":
                    if (currentPage < Pagination.TotalPages)
                        currentPage++;
                    break;
                case "last":
                    currentPage = Pagination.TotalPages;
                    break;
            }
            pageUrl += $"&pageIndex={currentPage}";
            VideoProcessor.Url = pageUrl;
            await LoadVideo();
        }

        private string GetUrl()
        {
            try
            {
                textData = FileHandler.ReadFromFile();
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return baseUrl;
            }
            UserName = textData[0];
            API_KEY = textData[1];
            IPAddress = IPGetter.GetIpOfHost();
            var url = $"{baseUrl}?psid={UserName}&accessKey={API_KEY}&clientIp={IPAddress}&limit=25";
            return url;
        }

    }
}
