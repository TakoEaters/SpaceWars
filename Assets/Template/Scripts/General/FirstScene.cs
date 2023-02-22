using UnityEngine;
using UnityEngine.SceneManagement;

namespace Template.Scripts.General
{
    public class FirstScene : MonoBehaviour
    {
        void Start()
        {
            Initialize();
            LoadFirstScene();
        }
        private void Initialize()
        {

        }
        private void LoadFirstScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}
