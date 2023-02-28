using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using UnityEngine;

namespace _Project.Scripts.Common.Sensitivity
{
    public class SensitivityManager : MonoBehaviour, ISensitivityManager
    {
        private readonly List<ISensitivity> _sensitivities = new List<ISensitivity>();
        
        public void Register()
        {
            ServiceLocator.Current.Register<ISensitivityManager>(this);
        }

        public void Subscribe(ISensitivity sensitivity)
        {
            if (_sensitivities.Contains(sensitivity)) return;
            _sensitivities.Add(sensitivity);
            sensitivity.SetSensitivity(SaveManager.GetSensitivityMultiplier());
        }
        
        public void ChangeSensitivity(float percentage)
        {
            SaveManager.SetSensitivityMultiplier(percentage);
            _sensitivities.ForEach(x => x.SetSensitivity(percentage));
        }

        public void UnSubscribe(ISensitivity sensitivity)
        {
            if (!_sensitivities.Contains(sensitivity)) return;
            _sensitivities.Remove(sensitivity);
        }
    }

    public interface ISensitivityManager : IGameService
    {
        public void Subscribe(ISensitivity sensitivity);
        public void UnSubscribe(ISensitivity sensitivity);
        public void ChangeSensitivity(float percentage);
    }
}
