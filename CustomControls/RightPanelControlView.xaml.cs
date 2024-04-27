using System.Windows;
using System.Windows.Controls;

namespace CityOrganisations.CustomControls
{
    public partial class RightPanelControlView : UserControl
    {
        public static readonly DependencyProperty PanelTitleProperty = DependencyProperty.Register(nameof(PanelTitle),
            typeof(string), typeof(RightPanelControlView), new PropertyMetadata(string.Empty));

        public string PanelTitle
        {
            get => (string) GetValue(PanelTitleProperty);
            set => SetValue(PanelTitleProperty, value);
        }
        
        public RightPanelControlView()
        {
            InitializeComponent();
        }
    }
}