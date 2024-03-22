using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using CityOrganisations.Views;
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
        protected override void RegisterTypes(IContainerRegistry containerRegistry) {}

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}