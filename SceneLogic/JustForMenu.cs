using UnityEngine;
using UnityEngine.SceneManagement;

public class JustForMenu : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private NextLevel nextLevel;
    private void Start()
    {
        nextLevel = FindObjectOfType<NextLevel>();
    }
    public void OnClick()
    {
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
