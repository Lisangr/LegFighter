using UnityEngine;
using UnityEngine.UI;

public class LvlForCharacterProperties : MonoBehaviour
{
    [SerializeField] private Text lvlText;
    private int _currentLevel;
    
    void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        lvlText.text = "LVL: " + _currentLevel.ToString();
    }
}
