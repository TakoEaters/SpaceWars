using System;

namespace _Project.Scripts.General.Signals
{
    public struct ScoreChanger
    {
        public Team Team;
    }


    [Serializable] public class TeamScore
    {
        public int TotalAmount;
        public Team Team;

        public TeamScore(Team team, int amount)
        {
            Team = team;
            TotalAmount = amount;
        }
    }
    
    [Serializable] public enum Team { Blue, Red }
}