using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.InputHandlers;
using _Project.Scripts.General.Signals;
using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Gameplay
{
    public class SettingsViewHandler : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private View _mainView;

        private bool _isSettingsEnabled;
        private bool _isOtherScreenEnabled = true;


        private void Awake()
        {
            _exitButton.onClick.AddListener(() =>
            {
                ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
                LevelsManager.LoadMainMenu();
            });
            _resumeButton.onClick.AddListener(CloseSettings);
        }

        [Sub]
        private void OnPlayerInitialized(PlayerDeploy reference)
        {
            _isOtherScreenEnabled = false;
        }

        [Sub]
        private void OnEnableNonSettingsScreen(EnableNonSettingsScreen reference)
        {
            _isOtherScreenEnabled = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isSettingsEnabled) CloseSettings();
                else OpenSettings();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OpenSettings()
        {
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            ServiceLocator.Current.Get<IPlayerInputs>().DisableInputs();
            ServiceLocator.Current.Get<ICameraManager>().DisableCameraInput();
            _isSettingsEnabled = true;
            Cursor.visible = true;
            _mainView.Enable();
        }

        private void CloseSettings()
        {
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            ServiceLocator.Current.Get<IPlayerInputs>().EnableInputs();
            if (!_isOtherScreenEnabled) ServiceLocator.Current.Get<ICameraManager>().EnableCameraInput();
            _isSettingsEnabled = false;
            Cursor.visible = _isOtherScreenEnabled;
            _mainView.Disable();
        }
    }
}
