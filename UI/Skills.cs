using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField] private Text _skillPointsText;
    [SerializeField] private Text _strongText;
    [SerializeField] private Text _vitalityText;
    [SerializeField] private GameObject _statsPanel;
    private int _skillPoints;
    private int _strong;
    private int _vitality;

    //private Player _player; // Ссылка на игрока

    private void Start()
    {
        _statsPanel.SetActive(false);
        //_player = FindObjectOfType<Player>(); // Находим объект игрока
        LoadSkills();
        UpdateUI();

        Debug.Log("Всего скилпоинтов " + _skillPoints);
    }

    public void IncreaseStrong()
    {
        if (_skillPoints > 0)
        {
            _strong++;
            _skillPoints--;
            Debug.Log("Добавлена сила, осталось " + _skillPoints);
            SaveSkills();
            UpdateUI(); // Обновляем UI сразу после изменения
        }
    }

    public void IncreaseVitality()
    {
        if (_skillPoints > 0)
        {
            _vitality++;
            _skillPoints--;
            Debug.Log("Добавлена выносливость, осталось " + _skillPoints);
            SaveSkills();

            //_player.SetVitality(_vitality);
            UpdateUI(); // Обновляем UI сразу после изменения
        }
    }

    private void UpdateUI()
    {
        _skillPointsText.text = _skillPoints.ToString();
        _strongText.text = _strong.ToString();
        _vitalityText.text = _vitality.ToString();
    }

    private void SaveSkills()
    {
        PlayerPrefs.SetInt("SkillPoints", _skillPoints);
        PlayerPrefs.SetInt("Strong", _strong);
        PlayerPrefs.SetInt("Vitality", _vitality);
        PlayerPrefs.Save();
    }

    private void LoadSkills()
    {
        _skillPoints = PlayerPrefs.GetInt("SkillPoints", 0);
        _strong = PlayerPrefs.GetInt("Strong", 15);
        _vitality = PlayerPrefs.GetInt("Vitality", 25);
    }
}
