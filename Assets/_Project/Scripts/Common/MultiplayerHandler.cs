using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AI;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
using _Project.Scripts.Player;
using UnityEngine;
namespace _Project.Scripts.Common
{
    public class MultiplayerHandler : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private int _removeScore = 1;
        [SerializeField, Range(25, 150)] private int _necessaryWinScore = 500;
        [SerializeField, Range(1, 10)] private int _totalSessionMinutes = 5;
        
        private readonly List<TeamScore> _teams = new List<TeamScore>();
        private Coroutine _countRoutine;
        private IScoreSystem _score;

        private Team _playerTeam;
        private readonly int _secondsInMinute = 60;
        private int _remainingTime;
        private bool _isFinishInitialized;
        

        [Sub]
        private void InitializeTeams(StartLevel reference)
        {
            _playerTeam = Team.Blue;
            ServiceLocator.Current.Get<IPlayerSystem>().InitializeSystem();
            ServiceLocator.Current.Get<IBotSystem>().InitializeSystem(_playerTeam);

            StartCounter();
        }

        private void StartCounter()
        {
            _score = ServiceLocator.Current.Get<IScoreSystem>();
            _score.SetMaxScore(_necessaryWinScore);
        }

        [Sub]
        private void OnReceiveScore(ScoreChanger reference)
        {
            Team necessaryTeam = reference.Team;
            necessaryTeam = necessaryTeam == Team.Blue ? Team.Red : Team.Blue;
            
            
            TeamScore currentTeam = _teams.Find(x => x.Team == necessaryTeam);
            if (currentTeam == null)
            {
                currentTeam = new TeamScore(necessaryTeam);
                _teams.Add(currentTeam);
            }

            currentTeam.TotalAmount += _removeScore;
            currentTeam.TotalAmount = Mathf.Clamp(currentTeam.TotalAmount, 0, _necessaryWinScore);
            _score.SetTeamScore(necessaryTeam,  currentTeam.TotalAmount);
        }

        [Sub]
        private void OnStartCounter(StartCounter reference)
        {
            _countRoutine = StartCoroutine(Count());
        }

        private bool IsAnyWinner()
        {
            TeamScore winner = _teams.Find(x => x.TotalAmount == _necessaryWinScore);
            return winner != null;
        }

        private void SendWinner()
        {
            TeamScore necessaryTeam = _teams.Find(x => x.Team == _playerTeam);
            Signal.Current.Fire<FinishLevel>(new FinishLevel {IsWin = necessaryTeam.TotalAmount == 100});
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Count()
        {
            _remainingTime = _totalSessionMinutes * _secondsInMinute;
            WaitForSeconds waitTime = new WaitForSeconds(1f);

            while (_remainingTime > 0)
            {
                if (IsAnyWinner())
                {
                    SendWinner();
                    yield break;
                }
                _score.SetCounterTime(_remainingTime);
                _remainingTime -= 1;
                yield return waitTime;
            }

            
            List<TeamScore> ordered = _teams.OrderBy(x => x.TotalAmount).ToList();
            Signal.Current.Fire<FinishLevel>(new FinishLevel {IsWin = ordered.First().Team == _playerTeam});
        }
    }
    
    public struct StartCounter { }
}