using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoPromotionApi.Services;

namespace VideoPromotionApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string UserName { get; set; }
        public string API_KEY { get; set; }
        public string IPAddress { get; set; }
        public ApiHelper ApiHelper { get; set; }
        public VideoProcessor VideoProcessor { get; set; }
        public IPGetter IPGetter { get; set; }
        public FileHandler FileHandler { get; set; }
        private string[] textData;
        private string baseUrl;
        public MainWindow()
        {
            InitializeComponent();
            baseUrl = "https://pt.potawe.com/api/video-promotion/v1/list";
            ApiHelper = new ApiHelper(baseUrl);
            FileHandler = new FileHandler();
            IPGetter = new IPGetter();
            textData = FileHandler.ReadFromFile();
            UserName = textData[0];
            API_KEY = textData[1];
            IPAddress = IPGetter.GetIpOfHost();
            var url = $"{baseUrl}?psid={UserName}&pstool=421_1&accessKey={API_KEY}&ms_notrack=1&campaign_id=&" +
                $"type=&sexualOrientation=straight&forcedPerformers=&limit=25&" +
                $"primaryColor=%23A474C4&labelColor=%23FFFFFF&clientIp={IPAddress}";
            VideoProcessor = new VideoProcessor(ApiHelper, url);
            DataContext = this;
        }

        private async Task LoadVideo()
        {
            var videoModel = await VideoProcessor.LoadVideo();
            var substituteThumbnail = $"https:{videoModel.Data.Videos[0].ThumbImage}";
            var uriSource = new Uri(substituteThumbnail, UriKind.Absolute);
            videoImage.Source = new BitmapImage(uriSource);

        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadVideo();
        }
    }
}
