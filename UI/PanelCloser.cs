using UnityEngine;

public class PanelCloser : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 || Input.GetKey(KeyCode.Escape))
        {
            Vector2 clickPosition;

            if (Input.touchCount > 0)
            {
                clickPosition = Input.GetTouch(0).position;
            }
            else
            {
                clickPosition = Input.mousePosition;
            }

            RectTransform rectTransform = GetComponent<RectTransform>();

            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, clickPosition, Camera.main))
            {
                gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
