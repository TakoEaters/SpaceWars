using System;
using _Project.Scripts.Core.LocatorServices;
using CrazyGames;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public class AdsManager : MonoBehaviour, IAdsManager
    {
        [SerializeField] private bool _testMode = true;
        
        private float _lastInterstitialTime;
        
        public void Register()
        {
            ServiceLocator.Current.Register<IAdsManager>(this);
        }
        public bool CanShowRewarded()
        {
            return true;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void ShowRewarded(Action<bool> onFinish)
        {
            if (_testMode)
            {
                onFinish?.Invoke(true);
                return;
            }
            CrazyAds.Instance.beginAdBreakRewarded(
                () => onFinish?.Invoke(true), 
                () => onFinish?.Invoke(false));
        }

        public void ShowInterstitial()
        { 
            if (_testMode) return;
            CrazyAds.Instance.beginAdBreak();
        }

        public void ShowBanner()
        {
            
        }
    }

    public interface IAdsManager : IGameService
    {
        public bool CanShowRewarded();
        public void ShowRewarded(Action<bool> onFinish);
        public void ShowInterstitial();

        public void ShowBanner();
    }
}