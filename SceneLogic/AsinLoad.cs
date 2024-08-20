using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsinLoad : MonoBehaviour
{
    [SerializeField] private Slider _slider;    
    [SerializeField] private Text _sliderValueText;
    private string _sceneName;
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadNextScene());        
    }

    private IEnumerator LoadNextScene() 
    {
        _sceneName = PlayerPrefs.GetString("SceneName", _sceneName);
        AsyncOperation oper = SceneManager.LoadSceneAsync(_sceneName);

        while (!oper.isDone)
        {
            float progress = oper.progress / 0.9f;
            _slider.value = progress;
            _sliderValueText.text = progress.ToString();
            yield return null;
        }
    }
}
