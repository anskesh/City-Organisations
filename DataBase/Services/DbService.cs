using System.Collections.ObjectModel;
using System.Linq;
using CityOrganisations.Models;
using CityOrganisations.Repositories;

namespace CityOrganisations.DataBase.Services
{
    public class DbService
    {
        private IRepository<Organization> _organizationRepository;
        private IRepository<Branch> _branchRepository;

        public ObservableCollection<OrganizationModel> Organizations { get; private set; }
        public ObservableCollection<BranchModel> Branches { get; private set; }
        
        public DbService(IRepository<Organization> organizationRepository, IRepository<Branch> branchRepository)
        {
            _organizationRepository = organizationRepository;
            _branchRepository = branchRepository;

            Organizations = new ObservableCollection<OrganizationModel>();
            Branches = new ObservableCollection<BranchModel>();
            
            GenerateOrganizations();
        }

        private void GenerateOrganizations()
        {
            var organizations = _organizationRepository.Get();

            foreach (var organization in organizations)
            {
                OrganizationModel organizationModel = ConvertOrganization(organization);
                
                var branchModels = organizationModel.Branches;
                var branches = _branchRepository.Get(x => x.OrgId == organization.Id);
                
                foreach (var branch in branches)
                {
                    BranchModel branchModel = ConvertBranch(branch, organization.OrgName);
                    Branches.Add(branchModel);
                    branchModels.Add(branchModel);
                }
                
                Organizations.Add(organizationModel);
            }
        }

        private BranchModel ConvertBranch(Branch branch, string orgName) => new (branch.Id, orgName, branch.Director, branch.PhysicalAddress, branch.Region, branch.XPos, branch.YPos);
        private OrganizationModel ConvertOrganization(Organization organization) => new (organization.Id, organization.OrgName, organization.LegalAddress, organization.TaxId);
    }
}