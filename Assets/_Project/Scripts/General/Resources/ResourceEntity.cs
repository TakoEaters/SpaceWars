using UnityEngine;

namespace _Project.Scripts.General.Resources
{
    public abstract class ResourceEntity : MonoBehaviour
    {
        public abstract Resource Resource { get; }
        public abstract int Amount { get; }

        public abstract void Add(int amount, bool isTemporary);
    }
}