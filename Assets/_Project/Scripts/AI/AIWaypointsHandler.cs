using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using MyBox;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class AIWaypointsHandler : MonoBehaviour, IWaypointsPath
    {
        [SerializeField] private List<Transform> _targets = new List<Transform>();
        public void Register()
        {
            ServiceLocator.Current.Register<IWaypointsPath>(this);
        }

        public Transform GetFutureTarget()
        {
            return _targets.GetRandom();
        }
    }

    public interface IWaypointsPath : IGameService
    {
        public Transform GetFutureTarget();
    }
}
