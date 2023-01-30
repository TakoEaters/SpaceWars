using _Project.Scripts.General.Signals;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Interface
{
    public class ScoreTeamView : MonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private TextMeshProUGUI _amount;

        public Team TeamView => _team;
        
        private int _maxSessionScore;
        
        public void SetMaxScore(int maxScoreValue)
        {
            _maxSessionScore = maxScoreValue;
            _amount.text = 0 + " / " + _maxSessionScore;
        }

        public void SetCurrentScore(int score)
        {
            _amount.text = score + " / " + _maxSessionScore;
        }


    }
}
