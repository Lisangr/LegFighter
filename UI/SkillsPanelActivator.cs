using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsPanelActivator : MonoBehaviour
{

    [SerializeField] private GameObject _statsPanel;

    private void Start()
    {
        _statsPanel.SetActive(false);
    }
    public void OnClick()
    {
        _statsPanel.SetActive(true);
    }
}
