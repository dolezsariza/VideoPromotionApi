using System.Collections.Generic;
using System.Windows.Documents;

namespace VideoPromotionApi.Models
{
    public class Data
    {
        public List<Video> Videos { get; set; }
        public Pagination Pagination { get; set; }
    }
}