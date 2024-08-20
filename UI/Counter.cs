using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text _textCounter;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private int _neededKills;
    private int _counter;

    private void Start()
    {
        _winPanel.SetActive(false);
        _counter = 0;
        _textCounter.text = _counter.ToString();

        _neededKills = PlayerPrefs.GetInt("Vitality", 25);
    }
    private void OnEnable()
    {
        Enemy.OnEnemyDestroy += AddKills;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDestroy -= AddKills;
    }

    private void AddKills()
    {
        _counter++;
        _textCounter.text = _counter.ToString();
    }

    private void Update()
    {
        if (_counter >= _neededKills)
        {
            _winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
