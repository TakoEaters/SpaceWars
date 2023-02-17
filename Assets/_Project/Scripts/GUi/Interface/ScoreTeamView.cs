using _Project.Scripts.General.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Interface
{
    public class ScoreTeamView : MonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private Slider _slider;

        public Team TeamView => _team;
        
        private int _maxSessionScore;
        
        public void SetMaxScore(int maxScoreValue)
        {
            _maxSessionScore = maxScoreValue;
            _amount.text = _maxSessionScore + "  <#afd9e9>/ " + maxScoreValue;
            _slider.value = (float)_maxSessionScore / maxScoreValue;
        }

        public void SetCurrentScore(int score)
        {
            _amount.text = score + " <#afd9e9>/ " + _maxSessionScore; 
            _slider.value = (float)score / _maxSessionScore;
        }
    }
}
