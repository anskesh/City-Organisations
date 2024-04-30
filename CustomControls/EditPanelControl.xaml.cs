using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;

namespace CityOrganisations.CustomControls
{
    public partial class EditPanelControl : UserControl
    {
        public static readonly DependencyProperty InfoContentProperty = DependencyProperty.Register(nameof(InfoContent),
            typeof(object), typeof(EditPanelControl), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty HeaderLabelProperty = DependencyProperty.Register(nameof(HeaderLabel),
            typeof(object), typeof(EditPanelControl),
            new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsEditModeProperty = DependencyProperty.Register(nameof(IsEditMode),
            typeof(bool), typeof(EditPanelControl), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty CanRemoveProperty = DependencyProperty.Register(nameof(CanRemove),
            typeof(bool), typeof(EditPanelControl), new PropertyMetadata(true));

        public static readonly DependencyProperty EditClickCommandProperty =
            DependencyProperty.Register(nameof(EditClickCommand), typeof(ICommand), typeof(EditPanelControl),
                new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty AddClickCommandProperty =
            DependencyProperty.Register(nameof(AddClickCommand), typeof(ICommand), typeof(EditPanelControl),
                new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty SaveClickCommandProperty =
            DependencyProperty.Register(nameof(SaveClickCommand), typeof(ICommand), typeof(EditPanelControl),
                new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty CancelClickCommandProperty =
            DependencyProperty.Register(nameof(CancelClickCommand), typeof(ICommand), typeof(EditPanelControl),
                new PropertyMetadata(default(ICommand)));

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

        public bool IsEditMode
        {
            get => (bool) GetValue(IsEditModeProperty);
            set => SetValue(IsEditModeProperty, value);
        }

        public bool CanRemove
        {
            get => (bool) GetValue(CanRemoveProperty);
            set => SetValue(CanRemoveProperty, value);
        }

        public ICommand AddClickCommand
        {
            get => (ICommand) GetValue(AddClickCommandProperty);
            set => SetValue(AddClickCommandProperty, value);
        }

        public ICommand EditClickCommand
        {
            get => (ICommand) GetValue(EditClickCommandProperty);
            set => SetValue(EditClickCommandProperty, value);
        }

        public ICommand SaveClickCommand
        {
            get => (ICommand) GetValue(SaveClickCommandProperty);
            set => SetValue(SaveClickCommandProperty, value);
        }

        public ICommand CancelClickCommand
        {
            get => (ICommand) GetValue(CancelClickCommandProperty);
            set => SetValue(CancelClickCommandProperty, value);
        }

        public ICommand OnAddClickCommand { get; }
        public ICommand OnEditClickCommand { get; }
        public ICommand OnSaveClickCommand { get; }
        public ICommand OnCancelClickCommand { get; }

        public EditPanelControl()
        {
            InitializeComponent();

            OnAddClickCommand = new DelegateCommand(ExecuteAddClickCommand);
            OnEditClickCommand = new DelegateCommand(ExecuteEditClickCommand);
            OnSaveClickCommand = new DelegateCommand(ExecuteSaveClickCommand);
            OnCancelClickCommand = new DelegateCommand(ExecuteCancelClickCommand);
        }

        private void ExecuteAddClickCommand()
        {
            if (AddClickCommand == null || !AddClickCommand.CanExecute(null))
                return;

            AddClickCommand.Execute(null);
            IsEditMode = true;
            CanRemove = false;
        }

        private void ExecuteEditClickCommand()
        {
            if (EditClickCommand == null || !EditClickCommand.CanExecute(null))
                return;

            EditClickCommand.Execute(null);
            IsEditMode = true;
            CanRemove = true;
        }

        private void ExecuteSaveClickCommand()
        {
            if (SaveClickCommand == null || !SaveClickCommand.CanExecute(null))
                return;

            SaveClickCommand.Execute(null);
            IsEditMode = false;
            CanRemove = true;
        }

        private void ExecuteCancelClickCommand()
        {
            if (CancelClickCommand == null || !CancelClickCommand.CanExecute(null))
                return;

            CancelClickCommand.Execute(null);
            IsEditMode = false;
            CanRemove = true;
        }
    }
}