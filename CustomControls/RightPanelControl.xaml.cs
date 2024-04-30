using System.Windows;
using System.Windows.Controls;
using CityOrganisations.Services.DataBase;
using CityOrganisations.Events;

namespace CityOrganisations.CustomControls
{
    public partial class RightPanelControl : UserControl
    {
        public static readonly DependencyProperty PanelTitleProperty = DependencyProperty.Register(nameof(PanelTitle), typeof(string), typeof(RightPanelControl), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(RightPanelControl), new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string PanelTitle
        {
            get => (string) GetValue(PanelTitleProperty);
            set => SetValue(PanelTitleProperty, value);
        }

        public int SelectedIndex
        {
            get => (int) GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public RightPanelControl()
        {
            InitializeComponent();
            EventService.EventAggregator.GetEvent<RefreshListEvent>().Subscribe(OnRefreshListEvent);
        }
        
        private void OnRefreshListEvent()
        {
            ListBox.Items.Refresh();
        }
    }
}