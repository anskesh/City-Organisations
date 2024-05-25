using CityOrganisations.Configuration;
using CityOrganisations.Dialogs;
using Prism.Ioc;
using Prism.Modularity;

namespace Common;

public class CommonModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<ConfigurationService>(() => new ConfigurationService("configuration.json"));

        // Диалоговые окна
        containerRegistry.RegisterDialog<ConfirmationDialog>();
        containerRegistry.RegisterDialog<InformationDialog>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}