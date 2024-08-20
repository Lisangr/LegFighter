using UnityEngine;
using UnityEngine.UI;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject[] objects;
    public Toggle[] toggles;
    public int currentIndex = 0;

    private void Start()
    {
        UpdateObjectStates();
    }

    public void SwitchToNextObject()
    {
        objects[currentIndex].SetActive(false);
        toggles[currentIndex].isOn = false; // Деактивируем текущий Toggle
        currentIndex++;
        if (currentIndex >= objects.Length)
        {
            currentIndex = 0;
        }
        objects[currentIndex].SetActive(true);
        toggles[currentIndex].isOn = true; // Активируем новый Toggle
    }

    public void SwitchToPreviousObject()
    {
        objects[currentIndex].SetActive(false);
        toggles[currentIndex].isOn = false; // Деактивируем текущий Toggle
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = objects.Length - 1;
        }
        objects[currentIndex].SetActive(true);
        toggles[currentIndex].isOn = true; // Активируем новый Toggle
    }

    private void UpdateObjectStates()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
            toggles[i].isOn = (i == currentIndex); // Обновляем состояние Toggle
        }
    }
}
