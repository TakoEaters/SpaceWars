using System.Collections.Generic;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using _Project.Scripts.General.Resources;
using _Project.Scripts.General.Saves;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.Resources
{
    public class ResourcesViewUI : MonoBehaviour
    {
        [SerializeField] private List<ResourceView> _views = new List<ResourceView>();
        
        [Sub]
        private void OnStartLevel(StartLevel reference)
        {
            _views.ForEach(x => x.Change(GetAmount(x.ViewResource)));
        }

        [Sub]
        private void OnChangeValue(UpdateResourceView reference)
        {
            ResourceView view = _views.Find(x => x.ViewResource == reference.Resource);
            view.Change(GetAmount(view.ViewResource));
        }

        private int GetAmount(Resource resource)
        {
            return SaveManager.GetResourcesAmount(resource);
        }
    }

    public struct UpdateResourceView
    {
        public Resource Resource;
    }
}