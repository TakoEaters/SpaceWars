using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.InputHandlers;
using _Project.Scripts.General.Signals;
using DG.Tweening;
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
        [SerializeField] private Button _saveButton;
        [SerializeField] private View _mainView;
        [SerializeField] private View _popUp;
        [SerializeField] private Image _background;

        private bool _isSettingsEnabled;
        private bool _isOtherScreenEnabled = true;
        private bool _isFinish;


        private void Awake()
        {
            _exitButton.onClick.AddListener(() =>
            {
                ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
                LevelsManager.LoadMainMenu();
            });
            _resumeButton.onClick.AddListener(CloseSettings);
            _settingsButton.onClick.AddListener(OpenSettingsPopUp);
            _saveButton.onClick.AddListener(CloseSettingsPopUp);
        }

        [Sub]
        private void OnFinish(FinishLevel reference)
        {
            _isOtherScreenEnabled = true;
            _isFinish = true;
            CloseSettings();
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
            if (_isFinish) return;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isSettingsEnabled) CloseSettings();
                else OpenSettings();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OpenSettings()
        {
            ServiceLocator.Current.Get<ILeaderboardView>().Disable();
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            ServiceLocator.Current.Get<IPlayerInputs>().DisableInputs();
            ServiceLocator.Current.Get<ICameraManager>().DisableCameraInput();
            _isSettingsEnabled = true;
            Cursor.visible = true;
            _mainView.Enable();
            _background.DOFade(0.8f, 0.25f);
        }

        private void OpenSettingsPopUp()
        {
            _mainView.Disable();
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            _popUp.Enable();
            _popUp.transform.localScale = Vector3.zero;
            _popUp.transform.DOScale(1f, 0.15f);
        }
        
        private void CloseSettingsPopUp()
        {
            _mainView.Enable();
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            _popUp.transform.DOScale(0f, 0.15f).OnComplete(() => _popUp.Disable());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CloseSettings()
        {
            ServiceLocator.Current.Get<ILeaderboardView>().Enable();
            _popUp.Disable();
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            ServiceLocator.Current.Get<IPlayerInputs>().EnableInputs();
            if (!_isOtherScreenEnabled) ServiceLocator.Current.Get<ICameraManager>().EnableCameraInput();
            _isSettingsEnabled = false;
            Cursor.visible = _isOtherScreenEnabled;
            _mainView.Disable();
            _background.DOFade(0f, 0.25f);
        }
    }
}
