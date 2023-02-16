using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using Template.Scripts.Core;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public void Initialize()
    {
        Application.targetFrameRate = 60;
        ServiceLocator.Current.Get<IGameHandler>().InitializeTeams();
    }

    private void Start()
    {
        Signal.Current.Fire<StartLevel>(new StartLevel());
    }
}
