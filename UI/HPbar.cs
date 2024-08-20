using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _defeatPanel;

    private Player _player;
    private void Start()
    {
        _defeatPanel.SetActive(false);
        _player = FindObjectOfType<Player>();
        UpdateHealthDisplay();
    }

    private IEnumerator DelayedPlayerSearch()
    {
        yield return new WaitForSeconds(1f);

        _player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        UpdateHealthDisplay();
    }
    private void UpdateHealthDisplay()
    {
        _healthBar.fillAmount = (float)_player.currentHealth / _player.maxHP;
        _hpText.text = $"{_player.currentHealth} / {_player.maxHP}";

        if (_player.currentHealth <= 0)
        {
            _defeatPanel.SetActive(true);
        }
    }
}
