using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private CinemachineVirtualCamera _deathCamera;
        
        private CinemachineBasicMultiChannelPerlin _cineMachineBasicMultiChannelPerlin;
        private float _startingIntensity;
        private float _shakeTimer;
        
        public void Register()
        {
            ServiceLocator.Current.Register<ICameraManager>(this);
            _cineMachineBasicMultiChannelPerlin =
                _playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera(float intensity, float time)
        {
            _cineMachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            _shakeTimer = time;
            _startingIntensity = intensity;
            StartCoroutine(LerpCamera());
        }

        public void OnEnableDeathCamera()
        {
            _playerCamera.gameObject.SetActive(false);
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

    public interface ICameraManager : IGameService
    {
        public void ShakeCamera(float intensity, float time);
        public void OnEnableDeathCamera();
    }
}
