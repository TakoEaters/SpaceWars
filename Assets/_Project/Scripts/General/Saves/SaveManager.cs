using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Resources;
using _Project.Scripts.GUi.MainMenu.Resources;
using UnityEngine;

namespace _Project.Scripts.General.Saves
{
    public static class SaveManager 
    {
        #region RESOURCES

        public static int GetResourcesAmount(Resource resource)
        {
            string id = $"Resource_{resource}_Amount";
            return PlayerPrefs.GetInt(id, 0);
        }

        public static void SetResourcesAmount(Resource resource, int amount)
        {
            string id = $"Resource_{resource}_Amount";
            PlayerPrefs.SetInt(id, amount);
        }

        public static void IncrementResourcesAmount(Resource resource, int incrementValue)
        {
            string id = $"Resource_{resource}_Amount";
            PlayerPrefs.SetInt(id, GetResourcesAmount(resource) + incrementValue);
            Signal.Current.Fire<UpdateResourceView>(new UpdateResourceView { Resource = resource });
        }

        #endregion
    }
}
