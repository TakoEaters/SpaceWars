using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.Pool;
using _Project.Scripts.Core.SaveSystem;
using _Project.Scripts.Core.SignalBus;
using Sirenix.Utilities;
using Template.Scripts.ParticlesUtils;
using UnityEngine;

namespace Template.Scripts.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        private List<IGameService> _initServices = new List<IGameService>();
        private IGameManager _gameManager;

        private void Awake()
        {
            PlayerMoney.Reset();
            ServiceLocator.Initialize();
            CorePool.Initialize();
            ParticleEmitter.Initialize();
            Signal.Initialize();

            FixDependencies();
        }

        private void FixDependencies()
        {
            var objects = FindObjectsOfType<MonoBehaviour>(true);
            objects.ForEach(x =>
            {
                switch (x)
                {
                    case IGameService gameService:
                        _initServices.Add(gameService);
                        gameService?.Register();
                        break;
                    case IGameManager gameManager:
                        _gameManager = gameManager;
                        break;
                }
            });
            _gameManager?.Initialize();
        }
    }

    public interface IGameManager
    {
        public void Initialize();
    }
}
