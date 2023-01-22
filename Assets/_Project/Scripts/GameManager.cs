using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using Template.Scripts.Core;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public void Initialize()
    {
        
    }

    private void Start()
    {
        Signal.Current.Fire<StartLevel>(new StartLevel());
    }
}
