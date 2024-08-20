using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        menuPanel.SetActive(false);
    }

    public void OnCLick()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
