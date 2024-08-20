using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLevelChanger : MonoBehaviour
{
    [SerializeField] private string _sceneName = "LevelChanger";
    private int _currentLevel = 1;
    public void LoadLevel()
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
