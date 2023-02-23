using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using _Project.Scripts.General.Utils;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.Settings
{
    public class GraphicsToggles : MonoBehaviour
    {
        [SerializeField] private List<GraphicsButton> _graphics = new List<GraphicsButton>();

        private void Start()
        {
            for (int i = 0; i < _graphics.Count; i++) _graphics[i].Initialize(IndexButtons, i);
            IndexButtons(SaveManager.GetGraphicsValue());
        }

        private void IndexButtons(int index)
        {
            for (int i = 0; i < _graphics.Count; i++)
            {
                if (index == i)
                {
                    _graphics[i].Enable();
                    ServiceLocator.Current.Get<IEffectsModifier>().SetQuality(i);
                    continue;
                }
               
                _graphics[i].Disable();
            }
        }
    }
}
