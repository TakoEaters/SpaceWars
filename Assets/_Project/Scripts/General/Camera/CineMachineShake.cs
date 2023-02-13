using System;
using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.General.Camera
{
    public class CineMachineShake : MonoBehaviour, ICameraShake
    {
        [SerializeField] private CinemachineVirtualCamera _cineMachineVirtual;

        private CinemachineBasicMultiChannelPerlin _cineMachineBasicMultiChannelPerlin;
        private float _startingIntensity;
        private float _shakeTimer;
        
        public void Register()
        {
            ServiceLocator.Current.Register<ICameraShake>(this);
            _cineMachineBasicMultiChannelPerlin =
                _cineMachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shake(float intensity, float time)
        {
            _cineMachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            _shakeTimer = time;
            _startingIntensity = intensity;
            StartCoroutine(LerpCamera());
        }
        
        private IEnumerator LerpCamera()
        {
            float timeElapsed = 0f;
            float endValue = 0f;

            while (timeElapsed < _shakeTimer)
            {
                _cineMachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(_startingIntensity, endValue, timeElapsed / _shakeTimer);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _cineMachineBasicMultiChannelPerlin.m_AmplitudeGain = endValue;
        }
    }

    public interface ICameraShake : IGameService
    {
        public void Shake(float intensity, float time);
    }
}
