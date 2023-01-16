using UnityEngine;
using UnityEngine.SceneManagement;

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
