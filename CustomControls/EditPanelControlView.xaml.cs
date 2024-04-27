using System.Windows;
using System.Windows.Controls;

namespace CityOrganisations.CustomControls
{
    public partial class EditPanelControlView : UserControl
    {
        public static readonly DependencyProperty InfoContentProperty = DependencyProperty.Register(nameof(InfoContent),
            typeof(object), typeof(EditPanelControlView), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty HeaderLabelProperty = DependencyProperty.Register(nameof(HeaderLabel),
            typeof(object), typeof(EditPanelControlView),
            new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public EditPanelControlView()
        {
            InitializeComponent();
        }

        public object InfoContent
        {
            get => GetValue(InfoContentProperty);
            set => SetValue(InfoContentProperty, value);
        }

        public object HeaderLabel
        {
            get => GetValue(HeaderLabelProperty);
            set => SetValue(HeaderLabelProperty, value);
        }
    }
}