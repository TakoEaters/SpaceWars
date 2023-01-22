using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using UnityEngine;

public class MultiplayerHandler : MonoBehaviour, IGameHandler
{
    private Coroutine _countRoutine;
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
    
    public void StartCounter()
    {
        _countRoutine = StartCoroutine(Count());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Count()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1f);

        while (_remainingTime > 0)
        {
            _remainingTime -= 1;
            yield return waitTime;
        }
    }
    
    
}

public interface IGameHandler : IGameService
{
    
}
