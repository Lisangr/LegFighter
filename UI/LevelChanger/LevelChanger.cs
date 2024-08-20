using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Button[] buttons;      
    public Image[] buttonImages;    
    public Sprite unlockedSprite;   
    private int _levelUnLock;

    void Start()
    {
        _levelUnLock = PlayerPrefs.GetInt("levels", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false; 
        }

        for (int i = 0; i < _levelUnLock; i++)
        {
            buttons[i].interactable = true; 
            buttonImages[i].sprite = unlockedSprite; 

            Transform childImage = buttons[i].transform.Find("Image");
            if (childImage != null)
            {
                childImage.gameObject.SetActive(false);
            }
        }
    }

    public void loadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex); 
    }
}
