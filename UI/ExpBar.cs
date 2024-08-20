using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image _expImage;
    [SerializeField] private TextMeshProUGUI _expText;
    [SerializeField] private Text _lvlText; // Поле для отображения уровня
    private int _expForLevelUp = 400;
    private int _currentExp;
    private int _currentLevel = 1; // Текущий уровень
    private int _skillPoints; // Количество скилл-поинтов

    private void Start()
    {
        LoadExperience();
        UpdateUI();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += AddExperience;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= AddExperience;
    }

    private void AddExperience(int exp)
    {
        _currentExp += exp;

        // Повышение уровня, если достигнут требуемый опыт
        while (_currentExp >= _expForLevelUp)
        {
            LevelUp();
        }

        SaveExperience();
        UpdateUI();
    }

    private void LevelUp()
    {
        _currentExp -= _expForLevelUp; // Перенос остатка опыта на следующий уровень
        _currentLevel++; // Увеличение уровня        
        _expForLevelUp = Mathf.CeilToInt(_expForLevelUp * 1.2f); // Увеличение опыта для следующего уровня на 20%

        _skillPoints = PlayerPrefs.GetInt("SkillPoints", 0);
        _skillPoints += 4; // Добавляем 4 скилл-поинта за каждый уровень
        PlayerPrefs.SetInt("SkillPoints", _skillPoints); // Сохранение скилл-поинтов
        PlayerPrefs.Save(); // Сохраняем изменения
        
    }

    private void UpdateUI()
    {
        _expImage.fillAmount = (float)_currentExp / _expForLevelUp;
        _expText.text = "EXP: " + _currentExp + "/" + _expForLevelUp;
        _lvlText.text = _currentLevel.ToString(); // Обновление текста уровня
        // Добавьте вывод скилл-поинтов на UI, если требуется
    }

    private void SaveExperience()
    {
        PlayerPrefs.SetInt("CurrentExp", _currentExp);
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel); // Сохранение уровня
        PlayerPrefs.SetInt("ExpForLevelUp", _expForLevelUp); // Сохранение опыта для следующего уровня
        //PlayerPrefs.SetInt("SkillPoints", _skillPoints); // Сохранение скилл-поинтов
        PlayerPrefs.Save(); // Сохраняем изменения
    }

    private void LoadExperience()
    {
        _currentExp = PlayerPrefs.GetInt("CurrentExp", 0); // Загружаем опыт, если его нет, то 0
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Загружаем уровень, если его нет, то 1
        _expForLevelUp = PlayerPrefs.GetInt("ExpForLevelUp", _expForLevelUp); // Загружаем требуемый опыт для уровня        
    }
}
