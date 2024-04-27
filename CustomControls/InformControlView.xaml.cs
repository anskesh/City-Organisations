using System.Windows;
using System.Windows.Controls;

namespace CityOrganisations.CustomControls.CustomInformationControl
{
    public partial class InformControlView : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label),
            typeof(string), typeof(RightPanelControlView), new PropertyMetadata(string.Empty));
        
        public static readonly DependencyProperty BoxTextProperty =
            DependencyProperty.Register(nameof(BoxText), typeof(string), typeof(InformControlView), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Label
        {
            get => (string) GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string BoxText
        {
            get => (string) GetValue(BoxTextProperty);
            set => SetValue(BoxTextProperty, value);
        }
        
        public InformControlView()
        {
            InitializeComponent();
        }
    }
}