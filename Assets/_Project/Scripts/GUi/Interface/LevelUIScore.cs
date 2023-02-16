using System;
using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using _Project.Scripts.General.Signals;
using Template.Scripts.General;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Interface
{
    public class LevelUIScore : MonoBehaviour, IScoreSystem
    {
        [SerializeField] private List<ScoreTeamView> _scoreViews = new List<ScoreTeamView>();
        [SerializeField] private TextMeshProUGUI _currentTime;
        [SerializeField] private View _view;

        public void Register()
        {
            ServiceLocator.Current.Register<IScoreSystem>(this);
        }
        
        [Sub]
        private void OnPlayerRevive(PlayerDeploy reference)
        {
            _view.Enable();
        }

        [Sub]
        private void OnPlayerDeath(PlayerDeath reference)
        {
            _view.Disable();
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

        public void SetMaxScore(int maxScore)
        {
            _scoreViews.ForEach(x => x.SetMaxScore(maxScore));
        }

        public void SetTeamScore(TeamScore currentTeam)
        {
            ScoreTeamView currentView = _scoreViews.Find(x => x.TeamView == currentTeam.Team);
            currentView.SetCurrentScore(currentTeam.TotalAmount);
        }
    }

    public interface IScoreSystem : IGameService
    {
        public void SetCounterTime(int time);
        public void SetMaxScore(int maxScore);
        public void SetTeamScore(TeamScore currentTeam);
    }
}