using CityOrganisations.Models;
using CityOrganisations.Services.DataBase;
using Prism.Events;
using Prism.Services.Dialogs;

namespace CityOrganisations.ViewModels
{
    public class HomePageViewModel : BaseEditableViewModel<BranchModel>
    {
        public HomePageViewModel(IDatabaseService<BranchModel> databaseService, IDialogService dialogService, IEventAggregator eventAggregator) : 
            base(databaseService, dialogService, eventAggregator) {}
        
        protected override BranchModel CreateItemCopy(BranchModel item)
        {
            throw new System.NotImplementedException();
        }

        protected override bool OnSaveAdditionalCheck()
        {
            throw new System.NotImplementedException();
        }
    }
}