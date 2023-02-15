using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerSystem : MonoBehaviour, IPlayerSystem
    {
        [SerializeField] private Player _player;
        
        public void Register()
        {
            ServiceLocator.Current.Register<IPlayerSystem>(this);
        }

        public void InitializeSystem()
        {
            _player.Initialize();
        }
    }

    public interface IPlayerSystem : IGameService
    {
        public void InitializeSystem();
    }
}
