using Other.Services.ServiceLocator;
using Other.Services.StaticDataService;

namespace Other.Game
{
    public class GameEntryPoint
    {
        public GameEntryPoint()
        {
            var container = AllServices.Container;
            
            RegisterStaticDataService(container);
        }

        private static void RegisterStaticDataService(AllServices container)
        {
            IStaticDataService staticDataService = new StaticDataService();

            staticDataService.LoadStaticData();

            container.RegisterSingle(staticDataService);
        }
    }
}