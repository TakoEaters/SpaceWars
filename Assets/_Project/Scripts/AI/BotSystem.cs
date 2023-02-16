using System;
using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Signals;
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

        public void InitializeSystem(Team friendlyTeam)
        {
            foreach (Team team in (Team[]) Enum.GetValues(typeof(Team)))
            {
                if (team == friendlyTeam)
                {
                    for (int i = 0; i < _bots.Count / 2; i++) InitializeData(_bots[i], team);
                }

                else
                {
                    for (int i = _bots.Count / 2; i < _bots.Count; i++) InitializeData(_bots[i], team);
                }
            }
        }

        private void InitializeData(Bot bot, Team team)
        {
            bot.Initialize(team);
            bot.UpdateBotData();
            bot.EnableController();
        }
    }
    public interface IBotSystem : IGameService
    {
        public void InitializeSystem(Team friendlyTeam);
    }
}
