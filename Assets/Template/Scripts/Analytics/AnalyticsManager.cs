using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.Core.Analytics
{
    public class AnalyticsManager : MonoBehaviour, IAnalytics
    {
        public void Register() => ServiceLocator.Current.Register<IAnalytics>(this);

    } 

    public interface IAnalytics : IGameService
    {

    }
}