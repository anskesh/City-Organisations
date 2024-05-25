using System.Windows;
using CityOrganisations;
using CityOrganisations.Views;
using Common;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Container.Resolve<IRegionManager>()
                .RegisterViewWithRegion("Main", nameof(MainWindow));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog
                .AddModule<CommonModule>()
                .AddModule<CoreModule>();
        }
    }
}