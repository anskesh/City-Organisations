using CityOrganisations.ViewModels;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace CityOrganisations.Views
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void MouseRightButtonCanvas(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as HomePageViewModel;
            if (viewModel != null)
            {
                viewModel.HandleMouseRightButton(e, MapCanvas);
            }
        }
    }
}