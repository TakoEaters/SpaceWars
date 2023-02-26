using System.Collections;
using _Project.Scripts.Audio;
using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Resources;
using _Project.Scripts.General.Saves;
using _Project.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Interface
{
    public class FinishCompleteView : MonoBehaviour
    {
        [Header("Buttons")] 
        [SerializeField] private Button _onClaimButton;
        [SerializeField] private Button _onDoubleClaimButton;
        [SerializeField] private TextMeshProUGUI _coinsRewardAmount;
        [SerializeField] private TextMeshProUGUI _bulletsRewardAmount;

        private readonly int _delay = 3;
        
        private int _countDelay = 3;
        private int _totalCoinsAmount;
        private int _totalBulletsAmount;

        private void Awake()
        {
            _onClaimButton.onClick.AddListener(OnClaim);
            _onDoubleClaimButton.onClick.AddListener(OnDoubleClaim);
        }

        public void Enable(int additiveAmount, int bulletAdditive)
        {
            gameObject.SetActive(true);
            _totalCoinsAmount = ServiceLocator.Current.Get<IPlayerSystem>().PlayerReward + additiveAmount;
            _totalBulletsAmount = ServiceLocator.Current.Get<IPlayerSystem>().BulletReward + bulletAdditive;
            StartCoroutine(Count());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Count()
        {
            float elapsedTime = 0f;
            float duration = 1f;
            float initial = 0f;
            
            while (elapsedTime < duration)
            {
                _countDelay++;
                int value = (int)Mathf.Lerp(initial, _totalCoinsAmount, elapsedTime / duration);
                int bulletValue = (int)Mathf.Lerp(initial, _totalBulletsAmount, elapsedTime / duration);
                _coinsRewardAmount.text = value.ToString();
                _bulletsRewardAmount.text = bulletValue.ToString();
                if (_countDelay >= _delay) ServiceLocator.Current.Get<IFXEmitter>().PlayCountSound();
                else _countDelay = 0;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            _coinsRewardAmount.text = _totalCoinsAmount.ToString();
            _bulletsRewardAmount.text = _totalBulletsAmount.ToString();
        }
        
        private void OnClaim()
        {
            SaveManager.IncrementResourcesAmount(Resource.Coins, _totalCoinsAmount);
            SaveManager.IncrementResourcesAmount(Resource.Bullets, _totalBulletsAmount);
            LevelsManager.LoadMainMenu();
        }

        private void OnDoubleClaim()
        {
            SaveManager.IncrementResourcesAmount(Resource.Bullets, _totalBulletsAmount * 2);
            SaveManager.IncrementResourcesAmount(Resource.Coins, _totalCoinsAmount * 2);
            LevelsManager.LoadMainMenu();
        }
    }
}
