using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private string _sceneNameForCreate = "Stage1";
    public static bool isEnter;
    private void Start()
    {
        isEnter = false;
        int enterState = PlayerPrefs.GetInt("EnterState");
        if (enterState == 0)
        {
            isEnter=true;
        }
    }
    private void Update()
    {
        if (isEnter)
        {
            continueButton.interactable = false;
        }
    }
    public void OnClickStart()
    {
        Time.timeScale = 1f;
        isEnter=true;
        int enterState = 1;
        PlayerPrefs.SetInt("EnterState", enterState);
        PlayerPrefs.SetString("SceneName", _sceneNameForCreate);
        PlayerPrefs.Save();

        SceneManager.LoadScene("AsinLoad", LoadSceneMode.Single);
    }
    [SerializeField] private string _sceneNameForContinue = "Stage1";
    public void OnClickContinue()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("SceneName", _sceneNameForContinue);
        PlayerPrefs.Save();

        SceneManager.LoadScene("AsinLoad", LoadSceneMode.Single);
    }
}