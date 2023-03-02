using System.Collections.Generic;
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
    public class LeaderboardView : MonoBehaviour, ILeaderboardView
    {
        [SerializeField] private BotScoreData _prefab;
        [SerializeField] private BotScoreData _playerPrefab;
        [SerializeField] private RectTransform _redParent;
        [SerializeField] private RectTransform _blueParent;
        [SerializeField] private View _mainView;
        [SerializeField] private Image _background;

        private readonly List<BotScoreData> _redTeam = new List<BotScoreData>();
        private readonly List<BotScoreData> _blueTeam = new List<BotScoreData>();
        private BotScoreData _playerScore;

        private bool _isScoreEnabled;
        private bool _isOtherScreenEnabled = true;
        private bool _isFinish;


        public void Register()
        {
            ServiceLocator.Current.Register<ILeaderboardView>(this);
        }

        [Sub]
        private void OnFinish(FinishLevel reference)
        {
            _isOtherScreenEnabled = true;
            _isFinish = true;
            CloseTab(false);
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
            if (_isScoreEnabled) CloseTab(false);
        }

        private void Update()
        {
            if (_isFinish) return;
            if (_isOtherScreenEnabled) return;
            if (Input.GetKeyDown(KeyCode.Tab)) OpenTab();
            if (Input.GetKeyUp(KeyCode.Tab)) CloseTab();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OpenTab()
        {
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            ServiceLocator.Current.Get<IPlayerInputs>().DisableInputs();
            ServiceLocator.Current.Get<ICameraManager>().DisableCameraInput();
            _isScoreEnabled = true;
            Cursor.visible = true;
            _mainView.Enable();
            _background.DOFade(0.8f, 0.25f);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CloseTab(bool withMusic = true)
        {
          if (withMusic)  ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            ServiceLocator.Current.Get<IPlayerInputs>().EnableInputs();
            if (!_isOtherScreenEnabled) ServiceLocator.Current.Get<ICameraManager>().EnableCameraInput();
            _isScoreEnabled = false;
            Cursor.visible = _isOtherScreenEnabled;
            _mainView.Disable();
            _background.DOFade(0f, 0.25f);
        }


        public void Enable()
        {
            _isOtherScreenEnabled = false;
        }

        public void Disable()
        {
            _isOtherScreenEnabled = true;
            if (_isScoreEnabled) CloseTab(false);
        }

        public void AddView(Team team, string nickname)
        {
            if (nickname == "Player")
            {
                BotScoreData player = Instantiate(_playerPrefab, _blueParent);
                player.Initialize(team, nickname);
                _blueTeam.Add(player);
                return;
            }
            RectTransform parent = team == Team.Blue ? _blueParent : _redParent;
            BotScoreData scoreView = Instantiate(_prefab, parent);
            scoreView.Initialize(team, nickname);
            if (team == Team.Blue) _blueTeam.Add(scoreView);
            else _redTeam.Add(scoreView);
        }

        public void UpdateData(Team team, string nickname)
        {
            var necessaryList =
                team == Team.Blue ? new List<BotScoreData>(_blueTeam) : new List<BotScoreData>(_redTeam);
            var data = necessaryList.Find(x => x.Nickname == nickname);
            data.IncrementScore();

            for (int i = necessaryList.Count - 1; i > -1; i--)
            {
                var current = necessaryList[i];
                int currentSiblingIndex = current.transform.GetSiblingIndex();
                int dataSiblingIndex = data.transform.GetSiblingIndex();
                if (data.Score <= current.Score || currentSiblingIndex > dataSiblingIndex) continue;
                
                current.transform.SetSiblingIndex(dataSiblingIndex);
                data.transform.SetSiblingIndex(currentSiblingIndex);
            }
        }
    }
    public interface ILeaderboardView : IGameService
    {
        public void Enable();
        public void Disable();
        public void AddView(Team team, string nickname);
        public void UpdateData(Team team, string nickname);
    }
}
