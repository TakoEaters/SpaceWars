using UnityEngine.SceneManagement;

namespace _Project.Scripts.Common
{
    public static class LevelsManager
    {
        public static void LoadMainMenu()
        {
            SceneManager.LoadScene(1);
        }

        public static void LoadGame()
        {
            SceneManager.LoadScene(2);
        }
    }
}