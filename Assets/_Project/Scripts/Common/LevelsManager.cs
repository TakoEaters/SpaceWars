using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.GUi;

namespace _Project.Scripts.Common
{
    public static class LevelsManager
    {
        public static void LoadMainMenu()
        {
            ServiceLocator.Current.Get<ILoadingScene>().Load(1);
        }

        public static void LoadGame()
        {
            ServiceLocator.Current.Get<ILoadingScene>().Load(2);
        }
    }
}