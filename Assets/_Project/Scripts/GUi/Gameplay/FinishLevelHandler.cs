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
        [SerializeField, Range(100, 500)] private int _completeAdditiveReward = 250;

        [Sub]
        private void OnFinish(FinishLevel reference)
        {
            if (reference.IsWin) _completeView.Enable(_completeAdditiveReward);
            else _failedView.Enable();
        }
    }
}
