using CityOrganisations.Models;
using CityOrganisations.Services.DataBase;
using Prism.Events;
using Prism.Services.Dialogs;

namespace CityOrganisations.ViewModels
{
    public class BranchesPageViewModel : BaseEditableViewModel<BranchModel>
    {
        public BranchesPageViewModel(IDatabaseService<BranchModel> databaseService, IDialogService dialogService, IEventAggregator eventAggregator) : 
            base(databaseService, dialogService, eventAggregator) {}

        protected override BranchModel CreateItemCopy(BranchModel item)
        {
            return new BranchModel(item);
        }

        protected override bool OnSaveAdditionalCheck() => true;
    }
}