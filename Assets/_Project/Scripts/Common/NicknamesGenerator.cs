using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public static class NicknamesGenerator
    {
        public static string GetNickName(Team team)
        {
            string nickname = string.Empty;
            char firstSymbol = 'R';
            if (team == Team.Blue) firstSymbol = 'B';
            int randomNumber = Random.Range(10, 50);
            nickname = firstSymbol + "-" + randomNumber;
            return nickname;
        }
    }
}
