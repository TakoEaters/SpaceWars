using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class PaintColor : MonoBehaviour, IPaintColor
    {
        [SerializeField] private Material _changeableMaterial;
        [SerializeField] private Color _necessaryColor;

        public Color CurrentColor => _necessaryColor;

        public void Register()
        {
            ServiceLocator.Current.Register<IPaintColor>(this);
            _changeableMaterial.color = _necessaryColor;
        }
    }

    public interface IPaintColor : IGameService
    {
        public Color CurrentColor { get; }
    }
}
