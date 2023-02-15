using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        [SerializeField, Range(0.1f, 2.0f)] private float _shakeIntensity = 0.55f;
        [SerializeField] private CinemachineVirtualCamera _gameplayCamera;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private CinemachineInputProvider _inputProvider;
        
        private CinemachineBasicMultiChannelPerlin _cineMachineBasicMultiChannelPerlin;
        private float _startingIntensity;
        private float _shakeTimer;
        
        public void Register()
        {
            ServiceLocator.Current.Register<ICameraManager>(this);
            _cineMachineBasicMultiChannelPerlin =
                _playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void EnableCameraInput()
        {
            _inputProvider.enabled = true;
        }

        public void EnableGameplayCamera()
        {
            _gameplayCamera.gameObject.SetActive(true);
        }

        public void ShakeCamera(float time)
        {
            _cineMachineBasicMultiChannelPerlin.m_AmplitudeGain = _shakeIntensity;
            _shakeTimer = time;
            _startingIntensity = _shakeIntensity;
            StartCoroutine(LerpCamera());
        }

        [Sub]
        private void OnEnableDeathCamera(PlayerDeath reference)
        {
            _inputProvider.enabled = false;
            _playerCamera.gameObject.SetActive(false);
        }

        [Sub]
        private void OnRevivePlayer(PlayerRevive reference)
        {
            _gameplayCamera.gameObject.SetActive(false);
            _playerCamera.gameObject.SetActive(true);
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
        public void EnableCameraInput();
        public void EnableGameplayCamera();
        public void ShakeCamera(float time);
    }
}
