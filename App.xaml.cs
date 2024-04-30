using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using CityOrganisations.Configuration;
using CityOrganisations.CustomControls;
using CityOrganisations.DataBase.Context;
using CityOrganisations.Services.DataBase;
using CityOrganisations.Dialogs;
using CityOrganisations.Models;
using CityOrganisations.Repositories;
using CityOrganisations.Views;
using Microsoft.EntityFrameworkCore;
using Prism;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;

namespace CityOrganisations
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ConfigurationService>(() => new ConfigurationService("configuration.json"));
            
            containerRegistry.RegisterSingleton<OrganizationContext>();
            containerRegistry.Register<IRepository<Organization>, DbRepository<Organization>>()
                .Register<IRepository<Branch>, DbRepository<Branch>>();
            
            containerRegistry.RegisterSingleton<OrganizationsService>();
            containerRegistry.Register<IDatabaseService<OrganizationModel>, OrganizationsService>();
            
            containerRegistry.RegisterSingleton<BranchesService>();
            containerRegistry.Register<IDatabaseService<BranchModel>, BranchesService>();
            
            containerRegistry.RegisterSingleton<CommonDatabaseService>();
            
            // Диалоговые окна
            containerRegistry.RegisterDialog<ConfirmationDialog>();
            containerRegistry.RegisterDialog<InformationDialog>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}