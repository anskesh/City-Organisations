using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace CityOrganisations.Dialogs
{
    public class BaseDialog : BindableBase, IDialogAware
    {
        public event Action<IDialogResult>? RequestClose;

        public DelegateCommand<string> CloseDialogCommand { get; }
        
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        
        private string _message;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        private string _title;

        public BaseDialog()
        {
            CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>(nameof(Message));
        }
        
        public virtual void OnDialogClosed() {}

        private void CloseDialog(string parameter)
        {
            DialogResult result = new (OnCloseDialog(parameter));
            RequestClose?.Invoke(result);
        }

        protected virtual ButtonResult OnCloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
                result = ButtonResult.Yes;
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            return result;
        }
    }
}