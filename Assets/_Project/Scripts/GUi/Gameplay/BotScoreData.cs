using _Project.Scripts.General.Signals;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Gameplay
{
    public class BotScoreData : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private TextMeshProUGUI _score;

        public string Nickname => _nickname.text;
        public int Score { get; protected set; }

        public void Initialize(Team team, string nick)
        {
            _nickname.text = nick;
            _nickname.color = team == Team.Blue ? Color.blue : Color.red;
            _score.text = "SCORE: " + Score;
        }

        public void IncrementScore()
        {
            Score++;
            _score.text = "SCORE: " + Score;
        }
    }
}
