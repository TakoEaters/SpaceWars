using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.AI;
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
        [SerializeField] private CinemachineVirtualCamera _deathCamera;
        [SerializeField] private List<CinemachineInputProvider> _inputProviders = new List<CinemachineInputProvider>();
        
        private CinemachineBasicMultiChannelPerlin _cineMachineBasicMultiChannelPerlin;
        private float _startingIntensity;
        private float _shakeTimer;
        
        public void Register()
        {
            _cineMachineBrain.m_DefaultBlend.m_Time = 1.0f;
            ServiceLocator.Current.Register<ICameraManager>(this);
            _cineMachineBasicMultiChannelPerlin =
                _playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void EnableCameraInput()
        {
            _cineMachineBrain.m_DefaultBlend.m_Time = 0.1f;
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
            _cineMachineBrain.m_DefaultBlend.m_Time = 1.0f;
            _inputProviders.ForEach(x => x.enabled = false); 
            ToggleDistance(false);
        }

        public void EnableGameplayCamera()
        {
            _deathCamera.Priority = 0;
            _gameplayCamera.Priority = 3;
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
            _aimingCamera.Priority = isChanged ? 2 : 0;
        }

        [Sub]
        private void OnEnableDeathCamera(PlayerDeath reference)
        {
            _deathCamera.Priority = 3;
            _cineMachineBrain.m_DefaultBlend.m_Time = 1.0f;
            Signal.Current.Fire<ToggleGameplayUI>(new ToggleGameplayUI {Enable = false});
            _inputProviders.ForEach(x => x.enabled = false);
        }

        [Sub]
        private void OnRevivePlayer(PlayerDeploy reference)
        {
            _gameplayCamera.Priority = 0;
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
