using Core.DataBase;
using Core.Models;
using Core.Repositories;
using Core.DataBase.Services;
using CityOrganisations.Views;
using Core.DataBase.Models;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CityOrganisations;

public class CoreModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Регионы
        containerRegistry.RegisterForNavigation<HomePage>();
        containerRegistry.RegisterForNavigation<BranchesPage>();
        containerRegistry.RegisterForNavigation<OrganizationsPage>();
        
        containerRegistry.RegisterSingleton<OrganizationContext>();
        containerRegistry.Register<IRepository<Organization>, DbRepository<Organization>>()
            .Register<IRepository<Branch>, DbRepository<Branch>>();
            
        containerRegistry.RegisterSingleton<OrganizationsService>();
        containerRegistry.Register<IDatabaseService<OrganizationModel>, OrganizationsService>();
            
        containerRegistry.RegisterSingleton<BranchesService>();
        containerRegistry.Register<IDatabaseService<BranchModel>, BranchesService>();

        containerRegistry.RegisterSingleton<CommonDatabaseService>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        containerProvider.Resolve<CommonDatabaseService>(); // ускоряем загрузку
        
        containerProvider.Resolve<IRegionManager>()
            .RegisterViewWithRegion(RegionNames.OrganizationsRegion, nameof(OrganizationsPage))
            .RegisterViewWithRegion(RegionNames.BranchesRegion, nameof(BranchesPage))
            .RegisterViewWithRegion(RegionNames.HomeRegion, nameof(HomePage));
    }
}