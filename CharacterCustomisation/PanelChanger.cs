using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    public GameObject[] panels;

    private void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void ChangePanel(int panelIndex)
    {
        if (panelIndex >= 0 && panelIndex < panels.Length)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (i != panelIndex)
                {
                    panels[i].SetActive(false);
                }
            }

            panels[panelIndex].SetActive(true);
        }
    }
}
