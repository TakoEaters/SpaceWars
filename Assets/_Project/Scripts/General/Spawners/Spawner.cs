using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.General.Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Team _team;

        public Vector3 SpawnPosition => transform.position;
        public Team TeamSpawner => _team;

        public void ChangeRandomTeam(Team team)
        {
            _team = team;
        }
    }
}
