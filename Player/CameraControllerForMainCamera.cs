using UnityEngine;

public class CameraControllerForMainCamera : MonoBehaviour
{
    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private float _xSpeed = 120.0f;
    [SerializeField] private float _ySpeed = 10.0f; // Уменьшено для мобильных устройств
    [SerializeField] private float _yMinLimit = 10f;
    [SerializeField] private float _yMaxLimit = 10f;

    private GameObject _touchArea;
    private float x = 0.0f;
    private float y = 0.0f;
    private Player _player;
    private Transform _playerTransform;
    private Camera _main;
    private Vector2 _touchStart;
    private bool _isDragging = false;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        if (_player != null)
        {
            _playerTransform = _player.GetComponent<Transform>();
        }
        _touchArea = GameObject.FindGameObjectWithTag("Toucharea");

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        RotateWithTouchArea();
    }

    void RotateWithTouchArea()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (RectTransformUtility.RectangleContainsScreenPoint(_touchArea.GetComponent<RectTransform>(), touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _touchStart = touch.position;
                        _isDragging = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isDragging)
                        {
                            Vector2 delta = touch.position - _touchStart;
                            _touchStart = touch.position;
                            x += delta.x * _xSpeed * Time.deltaTime;
                            y -= delta.y * _ySpeed * Time.deltaTime;

                            y = ClampAngle(y, _yMinLimit, _yMaxLimit);

                            Quaternion rotation = Quaternion.Euler(y, x, 0);
                            transform.rotation = rotation;

                            Vector3 position = _playerTransform.position + transform.forward * -_distance;
                            transform.position = position;
                        }
                        break;

                    case TouchPhase.Ended:
                        _isDragging = false;
                        break;
                }
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}