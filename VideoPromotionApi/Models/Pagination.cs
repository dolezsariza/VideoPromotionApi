using System.ComponentModel;

namespace VideoPromotionApi.Models
{
    public class Pagination : INotifyPropertyChanged
    {
        public int Total { get; set; }
        public int Count { get; set; }
		private int currentPage;
        public int CurrentPage 
		{
			get { return currentPage; }
			set
			{
				if (currentPage != value)
				{
					currentPage = value;
					NotifyPropertyChanged("CurrentPage");
				}
			}
		}
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
		

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propName)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
	}

}