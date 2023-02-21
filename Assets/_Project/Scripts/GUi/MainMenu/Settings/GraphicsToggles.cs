using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.Settings
{
    public class GraphicsToggles : MonoBehaviour
    {
        [SerializeField] private List<GraphicsButton> _graphics = new List<GraphicsButton>();

        private void Awake()
        {
            for (int i = 0; i < _graphics.Count; i++) _graphics[i].Initialize(IndexButtons, i);
            IndexButtons(2);
        }

        private void IndexButtons(int index)
        {
            for (int i = 0; i < _graphics.Count; i++)
            {
                if (index == i)
                {
                    _graphics[i].Enable();
                    continue;
                }
               
                _graphics[i].Disable();
            }
        }
    }
}
