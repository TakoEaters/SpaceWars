using _Project.Scripts.General.Resources;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.Resources.Entities
{
    public class CoinsView : ResourceView
    {
        [SerializeField] private TextMeshProUGUI _text;
        public override Resource ViewResource => Resource.Coins;
        public override void Change(int value)
        {
            _text.text = value.ToString();
        }
    }
}