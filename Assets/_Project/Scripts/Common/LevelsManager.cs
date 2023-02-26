using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.GUi;
using CrazyGames;

namespace _Project.Scripts.Common
{
    public static class LevelsManager
    {
        public static void LoadMainMenu()
        {
            CrazyEvents.Instance.GameplayStop();
            ServiceLocator.Current.Get<ILoadingScene>().Load(1);
        }

        public static void LoadGame()
        {
            ServiceLocator.Current.Get<IAdsManager>().ShowInterstitial();
            CrazyEvents.Instance.GameplayStart();
            ServiceLocator.Current.Get<ILoadingScene>().Load(2);
        }
    }
}