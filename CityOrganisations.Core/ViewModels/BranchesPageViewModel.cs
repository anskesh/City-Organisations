using CityOrganisations.Events;
using Core.Models;
using Core.DataBase.Services;
using Prism.Events;
using Prism.Services.Dialogs;

namespace CityOrganisations.ViewModels
{
    public class BranchesPageViewModel : BaseEditableViewModel<BranchModel>
    {
        public BranchesPageViewModel(IDatabaseService<BranchModel> databaseService, IDialogService dialogService, IEventAggregator eventAggregator) : 
            base(databaseService, dialogService, eventAggregator) {

            eventAggregator.GetEvent<RefreshListEvent>().Subscribe(() =>
            {
                RaisePropertyChanged(nameof(Items));
            });
        }

        protected override BranchModel CreateItemCopy(BranchModel item)
        {
            return new BranchModel(item);
        }

        protected override bool OnSaveAdditionalCheck() => true;
    }
}