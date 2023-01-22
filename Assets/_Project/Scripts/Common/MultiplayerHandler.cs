using System;
using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using _Project.Scripts.GUi.LevelHandlers.Common;
using UnityEngine;

public class MultiplayerHandler : MonoBehaviour, IGameHandler
{
    [SerializeField, Range(1, 10)] private int _totalSessionMinutes = 5;
    
    private Coroutine _countRoutine;
    private ICounterSystem _counter;
    private readonly int _secondsInMinute = 60;
    private int _remainingTime;
    
    public void Register()
    {
        ServiceLocator.Current.Register<IGameHandler>(this);
    }


    [Sub]
    private void OnStartLevel(StartLevel reference)
    {
        StartCounter();
    }
    
    private void StartCounter()
    {
        _counter = ServiceLocator.Current.Get<ICounterSystem>();
        _countRoutine = StartCoroutine(Count());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Count()
    {
        _remainingTime = _totalSessionMinutes * _secondsInMinute;
        WaitForSeconds waitTime = new WaitForSeconds(1f);

        while (_remainingTime > 0)
        {
            _counter.SetCounterTime(_remainingTime);
            _remainingTime -= 1;
            yield return waitTime;
        }
        
    }
}

public interface IGameHandler : IGameService
{
    
}
