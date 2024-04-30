using System.Linq;
using CityOrganisations.Models;
using CityOrganisations.Repositories;

namespace CityOrganisations.Services.DataBase
{
    public class BranchesService : DatabaseService<Branch, BranchModel>
    {
        public BranchesService(IRepository<Branch> repository) : base(repository) {}

        public void Initialize(OrganizationModel organization)
        {
            var branches = Repository.Get(x => x.OrgId == organization.Id);

            foreach (var branch in branches)
            {
                BranchModel branchModel = ConvertBranch(branch, organization);
                Items.Add(branchModel);
            }
        }

        private BranchModel ConvertBranch(Branch branch, OrganizationModel organization) =>
            new(branch.Id, organization.Id, organization.Name, branch.Director, branch.PhysicalAddress, branch.Region, branch.XPos, branch.YPos);

        public override void Add(BranchModel model)
        {
            AddEvent?.Invoke(model);
            BranchModel lastOrgModel = Items.LastOrDefault(x => x.OrgId == model.OrgId);

            if (lastOrgModel != null) // если элемент такой организации существует - вставляем после него
            {
                Items.Insert(Items.IndexOf(lastOrgModel) + 1, model);
            }
            else
            {
                Items.Add(model);
            }

            Branch newOrganization = new Branch(model);
            model.Id = Repository.Add(newOrganization); // устанавливаем id, который создался при добавлении в БД
        }

        public override void Update(BranchModel model, BranchModel newModel)
        {
            UpdateEvent?.Invoke(model, newModel);

            int index = Items.IndexOf(model);
            Items[index] = newModel;

            Branch branch = Repository.Get(x => x.Id == newModel.Id).First();
            branch.Copy(newModel);
            Repository.Update(branch);
        }

        public override void Remove(BranchModel model)
        {
            Items.Remove(model);
            Repository.Remove(model.Id);
            
            RemoveEvent?.Invoke(model);
        }
    }
}