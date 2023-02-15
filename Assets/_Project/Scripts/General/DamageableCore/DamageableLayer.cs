using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.General.DamageableCore
{
    [CreateAssetMenu(fileName = "DamageableLayer", menuName = "Configs/DamageableLayers/Layer")]
    public class DamageableLayer : ScriptableObject
    {
        [SerializeField] private List<DamageableLayer> _collisionLayers = new List<DamageableLayer>();

        public List<DamageableLayer> GetLayers()
        {
            return _collisionLayers;
        }

        public bool ContainsLayer(DamageableLayer layer)
        {
            return layer && _collisionLayers.Contains(layer);
        }
    }
}