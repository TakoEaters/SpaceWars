using System;
using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace Template.Scripts.Ads
{
    public class AdsManager : MonoBehaviour, IAdsManager 
    {
        #region Local
        public void Register() => ServiceLocator.Current.Register<IAdsManager>(this);
        private void Start() => InitAll();
        private void InitAll()
        {
            InitializeInterstitial();
            InitializeRewarded();
        }
        #endregion

        #region Interstitial
        private void InitializeInterstitial()
        {
        }
        #endregion

        #region Rewarded

        public void InitializeRewarded()
        {
        }

        #endregion

        #region API
        public bool CanShowRewarded()
        {
            return false;
        }

        public void ShowRewarded(Action<bool> onFinish)
        {
            onFinish?.Invoke(false);
        }

        public void ShowInterstitial()
        {

        }
        #endregion
    }

    public interface IAdsManager : IGameService
    {
        public bool CanShowRewarded();
        public void ShowRewarded(Action<bool> onFinish);
        public void ShowInterstitial();
    }
}