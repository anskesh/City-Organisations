using Prism.Services.Dialogs;

namespace CityOrganisations.Dialogs
{
    public class InformationDialogViewModel : BaseDialog
    {
        public InformationDialogViewModel()
        {
            Title = "Информация";
        }

        protected override ButtonResult OnCloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
                result = ButtonResult.Yes;
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.None;

            return result;
        }
    }
}