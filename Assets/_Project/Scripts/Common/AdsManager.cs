using System;
using _Project.Scripts.Core.LocatorServices;
using CrazyGames;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public class AdsManager : MonoBehaviour, IAdsManager
    {
        private float _lastInterstitialTime;
        
        public void Register()
        {
            ServiceLocator.Current.Register<IAdsManager>(this);
        }
        public bool CanShowRewarded()
        {
            return true;
        }

        public void ShowRewarded(Action<bool> onFinish)
        {
            CrazyAds.Instance.beginAdBreakRewarded(
                () => onFinish?.Invoke(true), 
                () => onFinish?.Invoke(false));
        }

        public void ShowInterstitial()
        { 
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