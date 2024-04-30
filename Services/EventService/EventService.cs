using Prism.Events;

namespace CityOrganisations.Services.DataBase
{
    public class EventService
    {
        public static IEventAggregator EventAggregator { get; private set; }

        public EventService(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }
    }
}