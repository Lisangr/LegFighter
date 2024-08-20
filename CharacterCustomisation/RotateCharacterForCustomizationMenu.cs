using UnityEngine;

public class RotateCharacterForCustomizationMenu : MonoBehaviour
{
    public static bool isDragging = false;
    private Vector3 _initialMousePosition;
    private float _rotationSpeed = 100.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            _initialMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float deltaX = currentMousePosition.x - _initialMousePosition.x;

            transform.Rotate(Vector3.up, -deltaX * _rotationSpeed * Time.deltaTime);
            _initialMousePosition = currentMousePosition;
        }
    }
}
