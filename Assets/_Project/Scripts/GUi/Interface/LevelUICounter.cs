using System;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using Template.Scripts.General;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.LevelHandlers.Common
{
    public class LevelUICounter : MonoBehaviour, ICounterSystem
    {
        [SerializeField] private TextMeshProUGUI _currentTime;
        [SerializeField] private View _view;
        
        public void Register()
        {
            ServiceLocator.Current.Register<ICounterSystem>(this);
        }
        
        [Sub]
        private void OnStart(StartLevel reference)
        {
            _view.Enable();
        }

        [Sub]
        private void OnFinish(FinishLevel reference)
        {
            _view.Disable();
        }

        public void SetCounterTime(int time)
        {
            TimeSpan ts = TimeSpan.FromSeconds(time);
            _currentTime.text = $"{ts.Minutes:00}:{ts.Seconds:00}";
        }
    }

    public interface ICounterSystem : IGameService
    {
        public void SetCounterTime(int time);
    }
}