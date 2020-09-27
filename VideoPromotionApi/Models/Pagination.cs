using System.ComponentModel;

namespace VideoPromotionApi.Models
{
    public class Pagination 
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
		
	}

}