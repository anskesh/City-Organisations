using CityOrganisations.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CityOrganisations.Views
{
    public partial class HomePage : Page
    {
        private double _scale = 1.0;

        public HomePage()
        {
            InitializeComponent();
        }

        private void MouseRightButtonCanvas(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as HomePageViewModel;
            viewModel.HandleMouseRightButton(e, MapCanvas);
        }

        private void MapScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var viewModel = (HomePageViewModel)DataContext;

            int delta = e.Delta > 0 ? 1 : -1;

            _scale += delta * 0.1;

            e.Handled = true;

            MapCanvas.LayoutTransform = new ScaleTransform(_scale, _scale);
        }
    }
}