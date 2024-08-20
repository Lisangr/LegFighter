using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image _expImage;
    [SerializeField] private TextMeshProUGUI _expText;
    [SerializeField] private Text _lvlText; // ���� ��� ����������� ������
    private int _expForLevelUp = 400;
    private int _currentExp;
    private int _currentLevel = 1; // ������� �������
    private int _skillPoints; // ���������� �����-�������

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

        // ��������� ������, ���� ��������� ��������� ����
        while (_currentExp >= _expForLevelUp)
        {
            LevelUp();
        }

        SaveExperience();
        UpdateUI();
    }

    private void LevelUp()
    {
        _currentExp -= _expForLevelUp; // ������� ������� ����� �� ��������� �������
        _currentLevel++; // ���������� ������        
        _expForLevelUp = Mathf.CeilToInt(_expForLevelUp * 1.2f); // ���������� ����� ��� ���������� ������ �� 20%

        _skillPoints = PlayerPrefs.GetInt("SkillPoints", 0);
        _skillPoints += 4; // ��������� 4 �����-������ �� ������ �������
        PlayerPrefs.SetInt("SkillPoints", _skillPoints); // ���������� �����-�������
        PlayerPrefs.Save(); // ��������� ���������
        
    }

    private void UpdateUI()
    {
        _expImage.fillAmount = (float)_currentExp / _expForLevelUp;
        _expText.text = "EXP: " + _currentExp + "/" + _expForLevelUp;
        _lvlText.text = _currentLevel.ToString(); // ���������� ������ ������
        // �������� ����� �����-������� �� UI, ���� ���������
    }

    private void SaveExperience()
    {
        PlayerPrefs.SetInt("CurrentExp", _currentExp);
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel); // ���������� ������
        PlayerPrefs.SetInt("ExpForLevelUp", _expForLevelUp); // ���������� ����� ��� ���������� ������
        //PlayerPrefs.SetInt("SkillPoints", _skillPoints); // ���������� �����-�������
        PlayerPrefs.Save(); // ��������� ���������
    }

    private void LoadExperience()
    {
        _currentExp = PlayerPrefs.GetInt("CurrentExp", 0); // ��������� ����, ���� ��� ���, �� 0
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // ��������� �������, ���� ��� ���, �� 1
        _expForLevelUp = PlayerPrefs.GetInt("ExpForLevelUp", _expForLevelUp); // ��������� ��������� ���� ��� ������        
    }
}
