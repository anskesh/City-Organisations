using System.Linq;
using CityOrganisations.Models;
using CityOrganisations.Repositories;
using Prism.Events;

namespace CityOrganisations.Services.DataBase
{
    public class OrganizationsService : DatabaseService<Organization, OrganizationModel>
    {
        private IEventAggregator _eventAggregator;
        
        public OrganizationsService(IRepository<Organization> repository, IEventAggregator eventAggregator) : base(repository)
        {
            _eventAggregator = eventAggregator;
            
            Initialize();
        }

        private void Initialize()
        {
            var organizations = Repository.Get();

            foreach (var organization in organizations)
            {
                OrganizationModel organizationModel = ConvertOrganization(organization);
                Items.Add(organizationModel);
            }
        }
        
        private OrganizationModel ConvertOrganization(Organization organization) => new (organization.Id, organization.OrgName, organization.LegalAddress, organization.TaxId);

        public override void Add(OrganizationModel model)
        {
            Items.Add(model);
            
            Organization newOrganization = new Organization(model);
            model.Id = Repository.Add(newOrganization); // устанавливаем id, который создался при добавлении в БД
            
            AddEvent?.Invoke(model);
        }

        public override void Update(OrganizationModel model, OrganizationModel newModel)
        {
            UpdateEvent?.Invoke(model, newModel); // обрабатываем изменения
            
            int index = Items.IndexOf(model);
            Items[index].Copy(newModel);

            Organization organization = Repository.Get(x => x.Id == model.Id).First();
            organization.Copy(model);
            Repository.Update(organization);
        }

        public override void Remove(OrganizationModel model)
        {
            RemoveEvent?.Invoke(model);
            
            Items.Remove(model);
            Repository.Remove(model.Id);
        }
    }
}