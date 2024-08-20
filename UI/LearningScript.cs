using System.Collections;
using UnityEngine;

public class LearningScript : MonoBehaviour
{
    [SerializeField] private GameObject _object1;
    [SerializeField] private GameObject _object2;
    [SerializeField] private GameObject _object3;

    private const string displayFlagKey = "ObjectsDisplayed";

    private void Awake()
    {
        _object1.SetActive(false);
        _object2.SetActive(false);
        _object3.SetActive(false);
    }
    void Start()
    {
        if (PlayerPrefs.GetInt(displayFlagKey, 0) == 1)
        {
            Time.timeScale = 1; 
            return;
        }

        Time.timeScale = 0;               

        StartCoroutine(ShowObjectsSequentially());
    }

    IEnumerator ShowObjectsSequentially()
    {
        _object1.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        _object1.SetActive(false);

        _object2.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        _object2.SetActive(false);

        _object3.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        _object3.SetActive(false);

        PlayerPrefs.SetInt(displayFlagKey, 1);
        PlayerPrefs.Save();

        Time.timeScale = 1;
    }
}
