using UnityEngine;

public class ButtonsForMaterialChanger : MonoBehaviour
{
    [SerializeField] private PanelChanger _panelChanger; 
    [SerializeField] private int _panelIndex;

    public void OnClick0()
    {
        _panelIndex = 0;
        _panelChanger.ChangePanel(_panelIndex);
    }
    public void OnClick1()
    {
        _panelIndex = 1;
        _panelChanger.ChangePanel(_panelIndex);
    }
    public void OnClick2()
    {
        _panelIndex = 2;
        _panelChanger.ChangePanel(_panelIndex);
    }
    public void OnClick3()
    {
        _panelIndex = 3;
        _panelChanger.ChangePanel(_panelIndex);
    }    
}