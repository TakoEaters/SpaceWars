using _Project.Scripts.General.Resources;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.Resources
{
    public abstract class ResourceView : MonoBehaviour
    {
        public abstract Resource ViewResource { get; }

        public abstract void Change(int value);
    }
}