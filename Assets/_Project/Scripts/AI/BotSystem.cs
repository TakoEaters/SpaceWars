using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class BotSystem : MonoBehaviour, IBotSystem
    {
        [SerializeField] private List<Bot> _bots = new List<Bot>();

        public void Register()
        {
            ServiceLocator.Current.Register<IBotSystem>(this);
        }

        public void InitializeSystem()
        {
            foreach (Bot ai in _bots)
            {
                ai.Initialize();
                ai.UpdateBotData();
                ai.EnableController();
            }
        }
    }

    public interface IBotSystem : IGameService
    {
        public void InitializeSystem();
    }
}
