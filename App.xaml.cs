using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using CityOrganisations.Configuration;
using CityOrganisations.DataBase.Context;
using CityOrganisations.DataBase.Services;
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
            
            containerRegistry.RegisterSingleton<DbService>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}