using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        [SerializeField, Range(0.1f, 2.0f)] private float _shakeIntensity = 0.55f;
        [SerializeField] private CinemachineBrain _cineMachineBrain;
        [SerializeField] private CinemachineVirtualCamera _gameplayCamera;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private CinemachineVirtualCamera _aimingCamera;
        [SerializeField] private List<CinemachineInputProvider> _inputProviders = new List<CinemachineInputProvider>();
        
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
            _cineMachineBrain.m_DefaultBlend.m_Time = 0.25f;
            Signal.Current.Fire<ToggleGameplayUI>(new ToggleGameplayUI {Enable = true});
            _inputProviders.ForEach(x => x.enabled = true);
        }

        [Sub]
        private void OnFinishLevel(FinishLevel reference)
        {
            _inputProviders.ForEach(x => x.enabled = false);
        }

        public void DisableCameraInput()
        {
            _inputProviders.ForEach(x => x.enabled = false);
        }

        public void EnableGameplayCamera()
        {
            _cineMachineBrain.m_DefaultBlend.m_Time = 1.0f;
            _gameplayCamera.gameObject.SetActive(true);
        }

        public void ShakeCamera(float time)
        {
            _cineMachineBasicMultiChannelPerlin.m_AmplitudeGain = _shakeIntensity;
            _shakeTimer = time;
            _startingIntensity = _shakeIntensity;
            StartCoroutine(LerpCamera());
        }

        public void ToggleDistance(bool isChanged)
        {
            _aimingCamera.gameObject.SetActive(isChanged);
        }

        [Sub]
        private void OnEnableDeathCamera(PlayerDeath reference)
        {
            _cineMachineBrain.m_DefaultBlend.m_Time = 1.0f;
            Signal.Current.Fire<ToggleGameplayUI>(new ToggleGameplayUI {Enable = false});
            _inputProviders.ForEach(x => x.enabled = false);
            _playerCamera.gameObject.SetActive(false);
            _aimingCamera.gameObject.SetActive(false);
        }

        [Sub]
        private void OnRevivePlayer(PlayerDeploy reference)
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
        public void DisableCameraInput();
        public void EnableGameplayCamera();
        public void ShakeCamera(float time);
        public void ToggleDistance(bool isChanged);
    }
}
