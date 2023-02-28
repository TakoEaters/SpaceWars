using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
using UnityEngine;

namespace _Project.Scripts.GUi.Gameplay
{
    public class FinishLevelHandler : MonoBehaviour
    {
        [SerializeField] private FinishCompleteView _completeView;
        [SerializeField] private FinishFailedView _failedView;
        [SerializeField, Range(100, 1000)] private int _coinsAdditiveReward = 250;
        [SerializeField, Range(10, 100)] private int _bulletAdditiveReward = 25;

        [Sub]
        private void OnFinish(FinishLevel reference)
        {
            if (reference.IsWin) _completeView.Enable(_coinsAdditiveReward, _bulletAdditiveReward);
            else _failedView.Enable();
        }
    }
}
