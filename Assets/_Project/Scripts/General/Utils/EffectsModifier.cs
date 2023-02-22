using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace _Project.Scripts.General.Utils
{
    public class EffectsModifier : MonoBehaviour, IEffectsModifier
    {
        [SerializeField] private VolumeProfile _volumeProfile;
        [SerializeField, Range(0.1f, 0.5f)] private float _maxVignetteRadius = 0.35f;
        
        private Vignette _vignette;

        public void Register()
        {
            ServiceLocator.Current.Register<IEffectsModifier>(this);
            int currentPreset = SaveManager.GetGraphicsValue();
            QualitySettings.SetQualityLevel(currentPreset);
            _volumeProfile.TryGet(out _vignette);
            _vignette.intensity.value = 0;
        }
        
        public void SetQuality(int value)
        {
            QualitySettings.SetQualityLevel(value);
            SaveManager.SetGraphicsValue(value);
        }

        public void UpdateVignette(float percentage)
        {
            _vignette.intensity.value = Mathf.Lerp(0, _maxVignetteRadius, percentage);
        }

        private void OnDestroy()
        {
            _vignette.intensity.value = 0;
        }
    }

    public interface IEffectsModifier : IGameService
    {
        public void UpdateVignette(float percentage);
        public void SetQuality(int value);
    }
}
