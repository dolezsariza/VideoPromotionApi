using System.Collections.Generic;

namespace VideoPromotionApi.Models
{
    public class Video
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public List<string> Tags { get; set; }
        public string ProfileImage { get; set; }
        public string ThumbImage { get; set; }
        public List<string> PreviewImages { get; set; }
        public string Quality { get; set; }
        public string Uploader { get; set; }
    }
}