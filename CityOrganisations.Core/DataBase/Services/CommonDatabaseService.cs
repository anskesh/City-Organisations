using System.Collections.Generic;
using System.Linq;
using CityOrganisations.Events;
using Core.Models;
using Prism.Events;

namespace Core.DataBase.Services
{
    public class CommonDatabaseService
    {
        private OrganizationsService _organizationService;
        private BranchesService _branchService;
        private readonly IEventAggregator _eventAggregator;

        public CommonDatabaseService(OrganizationsService organizationService, BranchesService branchService, IEventAggregator eventAggregator)
        {
            _organizationService = organizationService;
            _branchService = branchService;
            _eventAggregator = eventAggregator;
            
            _organizationService.UpdateEvent += OnUpdateOrganization;
            _organizationService.RemoveEvent += OnRemoveOrganization;
            
            _branchService.AddEvent += OnAddBranch;
            _branchService.UpdateEvent += OnUpdateBranch;

            GenerateBranches();
        }

        private void GenerateBranches()
        {
            foreach (var organization in _organizationService.Items)
                _branchService.Initialize(organization);
        }

        private void OnAddBranch(BranchModel model)
        {
            SetOrgId(model); // ищем нужный Id организации
        }
        
        private void OnUpdateBranch(BranchModel oldModel, BranchModel newModel)
        {
            if (oldModel.OrgName != newModel.OrgName) // так как имя организации обновилось, нужно обновить Id
                SetOrgId(newModel);
        }
        
        private void SetOrgId(BranchModel branch)
        {
            OrganizationModel organization = _organizationService.Items.FirstOrDefault(x => x.Name == branch.OrgName);

            if (organization == null)
            {
                organization = new OrganizationModel(0, branch.OrgName, "NULL", "NULL");
                _organizationService.Add(organization);
            }
            
            branch.OrgId = organization.Id;
            branch.OrgName = organization.Name;
        }

        private void OnUpdateOrganization(OrganizationModel oldModel, OrganizationModel newModel)
        {
            if (oldModel.Name != newModel.Name) // Если имя организации сменилось, нужно обновить это в филиалах
            {
                IEnumerable<BranchModel> branches = GetBranchesByOrgId(newModel.Id);
                
                foreach (var branch in branches)
                    branch.OrgName = newModel.Name;
                
                _eventAggregator.GetEvent<UpdateSelectedItemEvent>().Publish();
                _eventAggregator.GetEvent<RefreshListEvent>().Publish(); // вызываем ивент для обновления содержимого списков
            }
        }

        private void OnRemoveOrganization(OrganizationModel model) // удаляем все филиалы этой организации
        {
            List<BranchModel> branchesToRemove = GetBranchesByOrgId(model.Id).ToList();
            
            foreach (var branch in branchesToRemove)
                _branchService.Remove(branch);
        }
        
        private IEnumerable<BranchModel> GetBranchesByOrgId(int orgId) => _branchService.Items.Where(x => x.OrgId == orgId);
    }
}