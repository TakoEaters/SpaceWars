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
        [SerializeField] private TextMeshProUGUI _kills;
        [SerializeField] private TextMeshProUGUI _multiKills;
        [SerializeField] private TextMeshProUGUI _headshots;

        private readonly int _delay = 3;
        
        private int _countDelay = 3;
        private int _totalCoinsAmount;
        private int _totalBulletsAmount;
        private int _totalKills;
        private int _totalMultiKills;
        private int _totalHeadshots;

        private void Awake()
        {
            _onClaimButton.onClick.AddListener(OnClaim);
            _onDoubleClaimButton.onClick.AddListener(() => ServiceLocator.Current.Get<IAdsManager>().ShowRewarded(OnDoubleClaim));
        }

        public void Enable(int additiveAmount, int bulletAdditive)
        {
            gameObject.SetActive(true);
            var playerSystem = ServiceLocator.Current.Get<IPlayerSystem>();
            _totalKills = playerSystem.TotalKills;
            _totalHeadshots = playerSystem.TotalHeadshots;
            _totalMultiKills = playerSystem.TotalMultiKills;
            _totalCoinsAmount = playerSystem.PlayerReward + additiveAmount;
            _totalBulletsAmount = playerSystem.BulletReward + bulletAdditive;
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
                int killsValue = (int)Mathf.Lerp(initial, _totalKills, elapsedTime / duration);
                int multiKillsValue = (int)Mathf.Lerp(initial, _totalMultiKills, elapsedTime / duration);
                int headShotsValue = (int)Mathf.Lerp(initial, _totalHeadshots, elapsedTime / duration);
                _coinsRewardAmount.text = value.ToString();
                _bulletsRewardAmount.text = bulletValue.ToString();
                _kills.text = "KILLS - " + killsValue;
                _multiKills.text = "MULTIKILLS - " + multiKillsValue;
                _headshots.text = "HEADSHOTS - " + headShotsValue;
                if (_countDelay >= _delay) ServiceLocator.Current.Get<IFXEmitter>().PlayCountSound();
                else _countDelay = 0;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            _coinsRewardAmount.text = _totalCoinsAmount.ToString();
            _bulletsRewardAmount.text = _totalBulletsAmount.ToString();
            _kills.text = "KILLS - " + _totalKills;
            _headshots.text = "HEADSHOTS - " + _totalHeadshots;
            _multiKills.text = "MULTIKILLS - " + _totalMultiKills;
        }
        
        private void OnClaim()
        {
            SaveManager.IncrementResourcesAmount(Resource.Coins, _totalCoinsAmount);
            SaveManager.IncrementResourcesAmount(Resource.Bullets, _totalBulletsAmount);
            LevelsManager.LoadMainMenu();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OnDoubleClaim(bool showedRewarded)
        {
            if (showedRewarded)
            {
                SaveManager.IncrementResourcesAmount(Resource.Bullets, _totalBulletsAmount * 2);
                SaveManager.IncrementResourcesAmount(Resource.Coins, _totalCoinsAmount * 2);   
            }
            
            LevelsManager.LoadMainMenu();
        }
    }
}
