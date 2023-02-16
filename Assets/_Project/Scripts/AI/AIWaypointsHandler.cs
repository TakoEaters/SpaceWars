using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class AIWaypointsHandler : MonoBehaviour, IWaypointsPath
    {
        [SerializeField] private Transform _target;
        public void Register()
        {
            ServiceLocator.Current.Register<IWaypointsPath>(this);
        }

        public Transform GetFutureTarget()
        {
            return _target;
        }
    }

    public interface IWaypointsPath : IGameService
    {
        public Transform GetFutureTarget();
    }
}
