using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CityOrganisations.Views
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Border_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is Border border)
            {
                double currentScaleX = border.LayoutTransform.Value.M11;
                double currentScaleY = border.LayoutTransform.Value.M22;

                /*
                e.Delta - изменение колеса мыши. Если e.Delta положительное, то это значит,
                что колесо было прокручено вверх, а если отрицательное, то вниз
                */
                double scaleChange = e.Delta > 0 ? 0.1 : -0.1;

                double newScaleX = Math.Max(0.1, currentScaleX + scaleChange);
                double newScaleY = Math.Max(0.1, currentScaleY + scaleChange);

                border.LayoutTransform = new ScaleTransform(newScaleX, newScaleY);

                double newWidth = MapBorder.ActualWidth + newScaleX;
                double newHeight = MapBorder.ActualHeight + newScaleY;
                MapBorder.Width = newWidth;
                MapBorder.Height = newHeight;
            }
        }
    }
}