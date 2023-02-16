using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Signals;
using MyBox;
using UnityEngine;

namespace _Project.Scripts.General.Spawners
{
    public class SpawnersSystem : MonoBehaviour, ISpawnerSystem
    {
        private List<Spawner> _allSpawners = new List<Spawner>();


        public void Register()
        {
            ServiceLocator.Current.Register<ISpawnerSystem>(this);
            _allSpawners = GetComponentsInChildren<Spawner>().ToList();
        }

        public Spawner GetRandomSpawner(Team necessarySpawnTeam)
        {
            return _allSpawners.FindAll(x => x.TeamSpawner == necessarySpawnTeam).GetRandom();
        }
    }

    
    public interface ISpawnerSystem : IGameService
    {
        public Spawner GetRandomSpawner(Team necessarySpawnTeam);
    }
}
