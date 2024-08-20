using UnityEngine;

public class WillBeSoonPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject _wilbesoonPanel;

    private void Start()
    {
        _wilbesoonPanel.SetActive(false);
    }
    public void OnClick()
    {
        _wilbesoonPanel.SetActive(true);
    }
    public void OnButtonClick()
    {
        _wilbesoonPanel.SetActive(false);
    }
}
