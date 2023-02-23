using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Resources;
using _Project.Scripts.General.Saves;
using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Interface
{
    public class FinishFailedView : MonoBehaviour
    {
        [Header("Buttons")] 
        [SerializeField] private Button _onClaimButton;
        [SerializeField] private Button _onDoubleClaimButton;

        private int _totalReward;
        
        private void Awake()
        {
            _onClaimButton.onClick.AddListener(OnClaim);
            _onDoubleClaimButton.onClick.AddListener(OnDoubleClaim);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            _totalReward = ServiceLocator.Current.Get<IPlayerSystem>().PlayerReward;
        }
        
        private void OnClaim()
        {
            SaveManager.IncrementResourcesAmount(Resource.Coins, _totalReward);
            LevelsManager.LoadMainMenu();
        }

        private void OnDoubleClaim()
        {
            SaveManager.IncrementResourcesAmount(Resource.Coins, _totalReward * 2);
            LevelsManager.LoadMainMenu();
        }
    }
}
