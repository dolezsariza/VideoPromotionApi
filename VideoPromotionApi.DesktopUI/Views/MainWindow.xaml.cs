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

        private async void ChooseFilter_Click(object sender, RoutedEventArgs e)
        {
            var quality = Quality.Text.ToString().ToLower();
            var uploader = Uploader.Text.ToString();
            await _mwvm.FilterVideo(quality, uploader);
            
        }

        private void goToPreviousPage(object sender, RoutedEventArgs e)
        {

        }

        private void goToNextPage(object sender, RoutedEventArgs e)
        {

        }

        private void goToFirstPage(object sender, RoutedEventArgs e)
        {

        }
        private void goToLastPage(object sender, RoutedEventArgs e)
        {

        }
    }
}
