using System.Collections.Generic;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.GUi.Interface
{
    public class HealthUI : View
    {
        [SerializeField] private List<HeartUIElement> _healthPoints = new List<HeartUIElement>();

        private float _totalHealth = 100f;


        public void SetData(int total, int current)
        {
            _totalHealth = total;
            float step = (float)1 / _healthPoints.Count;
            for (int i = 0; i < _healthPoints.Count; i++) _healthPoints[i].Initialize(new Vector2(step * i, step * (i + 1))); 
            _healthPoints.ForEach(x => x.FillInactive((float)current / total));
        }
        
        public void ChangeUi(int currentAmount)
        {
            _healthPoints.ForEach(x => x.Fill(currentAmount / _totalHealth));
        }
    }

    public struct ChangeUIHealth
    {
        public int TotalHealth;
        public int CurrentHealth;
    }
}
