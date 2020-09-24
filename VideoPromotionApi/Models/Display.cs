using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace VideoPromotionApi.Models
{
    public class Display
    {
        public BitmapImage VideoImage { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Quality { get; set; }
        public string Uploader { get; set; }
        public List<string> Tags { get; set; }

        public Display(BitmapImage videoImage, string title, int duration, string quality, string uploader, List<string> tags)
        {
            VideoImage = videoImage;
            Title = title;
            Duration = duration;
            Quality = quality;
            Uploader = uploader;
            Tags = tags;
        }
    }
}
