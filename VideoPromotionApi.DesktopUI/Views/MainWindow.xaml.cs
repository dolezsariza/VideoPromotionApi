using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using VideoPromotionApi.DesktopUI.ViewModels;
using VideoPromotionApi.Services;

namespace VideoPromotionApi.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mwvm;

        public MainWindow()
        {
            InitializeComponent();
            _mwvm = new MainWindowViewModel();
            DataContext = _mwvm;

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _mwvm.LoadVideo();
            videoDataList.ItemsSource = _mwvm.DataToShow;
            Pagination.DataContext = _mwvm.Pagination;
        }

        private async void ChooseFilter_Clicked(object sender, RoutedEventArgs e)
        {
            var quality = Quality.Text.ToString().ToLower();
            var uploader = Uploader.Text.ToString();
            await _mwvm.FilterVideo(quality, uploader);
            Pagination.DataContext = _mwvm.Pagination;
            firstButton.IsEnabled = previousButton.IsEnabled = false;

        }

        private async void GoToFirstPage(object sender, RoutedEventArgs e)
        {
            await _mwvm.ChangePage("first");
            Pagination.DataContext = _mwvm.Pagination;
            previousButton.IsEnabled = firstButton.IsEnabled = false;
            nextButton.IsEnabled = lastButton.IsEnabled = true;
        }

        private async void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            await _mwvm.ChangePage("previous");
            Pagination.DataContext = _mwvm.Pagination;
            if (_mwvm.Pagination.CurrentPage == 1)
            {
                previousButton.IsEnabled = firstButton.IsEnabled = false;
            }
            nextButton.IsEnabled = lastButton.IsEnabled = true;
        }

        private async void GoToNextPage(object sender, RoutedEventArgs e)
        {
            await _mwvm.ChangePage("next");
            Pagination.DataContext = _mwvm.Pagination;
            if (_mwvm.Pagination.CurrentPage == _mwvm.Pagination.TotalPages)
            {
                previousButton.IsEnabled = lastButton.IsEnabled = false;
            }
            firstButton.IsEnabled = previousButton.IsEnabled = true;
        }

        private async void GoToLastPage(object sender, RoutedEventArgs e)
        {
            await _mwvm.ChangePage("last");
            Pagination.DataContext = _mwvm.Pagination;
            firstButton.IsEnabled = previousButton.IsEnabled = true;
            nextButton.IsEnabled = lastButton.IsEnabled = false;
        }

    }
}
