using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using Template.Scripts.Core;
using Template.Scripts.General;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts.GUi
{
    public class LoadingScreen : MonoBehaviour, ILoadingScene
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _percentage;
        [SerializeField] private View _view;
        
        
        public void Register()
        {
            ServiceLocator.Current.Register<ILoadingScene>(this);
        }
        
        public void Load(int sceneIndex)
        {
            _view.Enable();
            StartCoroutine(LoadSceneAsync(sceneIndex));
        }

        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            _slider.value = 0f;

            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                float result = operation.progress * 100;
                _percentage.text = "LOADING..." + result + "%";
                _slider.value = result;
                if (operation.progress >= 0.9f) operation.allowSceneActivation = true;

                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
            _view.Disable();
        }
    }

    public interface ILoadingScene : IGameService
    {
        public void Load(int sceneIndex);
    }
}