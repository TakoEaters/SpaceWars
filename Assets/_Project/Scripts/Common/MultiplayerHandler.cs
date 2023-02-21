using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.AI;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
using _Project.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Common
{
    public class MultiplayerHandler : MonoBehaviour
    {
        [SerializeField, Range(5, 25)] private int _removeScore = 15;
        [SerializeField, Range(100, 1000)] private int _necessaryWinScore = 500;
        [SerializeField, Range(1, 10)] private int _totalSessionMinutes = 5;
        
        private readonly List<TeamScore> _teams = new List<TeamScore>();
        private Coroutine _countRoutine;
        private IScoreSystem _score;

        private readonly int _secondsInMinute = 60;
        private int _remainingTime;
        

        [Sub]
        private void InitializeTeams(StartLevel reference)
        {
            Team playerTeam = (Team)Random.Range(0, 2);
            ServiceLocator.Current.Get<IPlayerSystem>().InitializeSystem(playerTeam);
            ServiceLocator.Current.Get<IBotSystem>().InitializeSystem(playerTeam);

            StartCounter();
        }

        private void StartCounter()
        {
            _score = ServiceLocator.Current.Get<IScoreSystem>();
            _score.SetMaxScore(_necessaryWinScore);
            _countRoutine = StartCoroutine(Count());
        }

        [Sub]
        private void OnReceiveScore(ScoreChanger reference)
        {
            TeamScore currentTeam = _teams.Find(x => x.Team == reference.Team);
            if (currentTeam == null)
            {
                currentTeam = new TeamScore(reference.Team, _necessaryWinScore);
                _teams.Add(currentTeam);
            }

            currentTeam.TotalAmount -= _removeScore;
            currentTeam.TotalAmount = Mathf.Clamp(currentTeam.TotalAmount, 0, _necessaryWinScore);
            _score.SetTeamScore(currentTeam);
        }

        private bool IsAnyWinner()
        {
            TeamScore winner = _teams.Find(x => x.TotalAmount <= 0);
            return winner != null;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Count()
        {
            _remainingTime = _totalSessionMinutes * _secondsInMinute;
            WaitForSeconds waitTime = new WaitForSeconds(1f);

            while (_remainingTime > 0)
            {
                if (IsAnyWinner()) yield break;
                _score.SetCounterTime(_remainingTime);
                _remainingTime -= 1;
                yield return waitTime;
            }
        }
    }
}