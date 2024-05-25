using System.Linq;
using Core.DataBase.Services;
using CityOrganisations.Dialogs;
using Core.Models;
using Prism.Events;
using Prism.Services.Dialogs;

namespace CityOrganisations.ViewModels
{
    public class OrganizationsPageViewModel : BaseEditableViewModel<OrganizationModel>
    {
        public OrganizationsPageViewModel(IDatabaseService<OrganizationModel> databaseService, IDialogService dialogService, IEventAggregator eventAggregator) : 
            base(databaseService, dialogService, eventAggregator) {}
        
        protected override OrganizationModel CreateItemCopy(OrganizationModel item)
        {
            return new OrganizationModel(item);
        }

        protected override bool OnSaveAdditionalCheck()
        {
            if (Items.Any(x => x.Name == SelectedItem.Name))
            {
                DialogService.ShowDialog(nameof(InformationDialog), new DialogParameters("Message=Организация с таким именем уже существует"), _ => {});
                return false;
            }

            return true;
        }
    }
}