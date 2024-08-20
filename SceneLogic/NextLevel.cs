using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Stage1";
    private int _currentLevel = 1;
    public void LoadNextLevel()
    {
        UnLockLevel();

        PlayerPrefs.SetString("SceneName", _sceneName);
        PlayerPrefs.Save();

        SceneManager.LoadScene("AsinLoad", LoadSceneMode.Single);
        Time.timeScale = 1.0f;
    }
    public void UnLockLevel()
    {
        _currentLevel = PlayerPrefs.GetInt("levels", _currentLevel);
        if (_currentLevel >= PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", _currentLevel + 1);
            PlayerPrefs.Save();
        }
    }
}
