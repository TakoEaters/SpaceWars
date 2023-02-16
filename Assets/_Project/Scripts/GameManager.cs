using _Project.Scripts.AI;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using _Project.Scripts.Player;
using Template.Scripts.Core;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public void Initialize()
    {
        Application.targetFrameRate = 60;
        ServiceLocator.Current.Get<IBotSystem>().InitializeSystem();
        ServiceLocator.Current.Get<IPlayerSystem>().InitializeSystem();
    }

    private void Start()
    {
        Signal.Current.Fire<StartLevel>(new StartLevel());
    }
}
