using UnityEngine;
using UnityEngine.UI;

public class ScaleObject : MonoBehaviour
{
    [SerializeField] private Slider _sliderX;
    [SerializeField] private Slider _sliderY;
    [SerializeField] private Slider _sliderZ;

    [SerializeField] private string _sceneName = "FirstScene";
    private float _minScale = 200f;
    private float _maxScale = 400f;
    private float _defaultScale = 300f;

    Vector3 scale;
    void Start()
    {
        float savedScaleX = PlayerPrefs.GetFloat("ScaleX", _defaultScale);
        float savedScaleY = PlayerPrefs.GetFloat("ScaleY", _defaultScale);
        float savedScaleZ = PlayerPrefs.GetFloat("ScaleZ", _defaultScale);

        transform.localScale = new Vector3(savedScaleX, savedScaleY, savedScaleZ);

        _sliderX.value = Mathf.InverseLerp(_minScale, _maxScale, savedScaleX);
        _sliderY.value = Mathf.InverseLerp(_minScale, _maxScale, savedScaleY);
        _sliderZ.value = Mathf.InverseLerp(_minScale, _maxScale, savedScaleZ);

        _sliderX.onValueChanged.AddListener(ChangeScaleX);
        _sliderY.onValueChanged.AddListener(ChangeScaleY);
        _sliderZ.onValueChanged.AddListener(ChangeScaleZ);

        PlayerPrefs.SetString("SceneName", _sceneName);
    }

    public void ChangeScaleX(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Lerp(_minScale, _maxScale, value);
        transform.localScale = scale;

        PlayerPrefs.SetFloat("ScaleX", scale.x);
        PlayerPrefs.Save();
    }

    public void ChangeScaleY(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Lerp(_minScale, _maxScale, value);
        transform.localScale = scale;

        PlayerPrefs.SetFloat("ScaleY", scale.y);
        PlayerPrefs.Save();
    }

    public void ChangeScaleZ(float value)
    {
        RotateCharacterForCustomizationMenu.isDragging = false;
        Vector3 scale = transform.localScale;
        scale.z = Mathf.Lerp(_minScale, _maxScale, value);
        transform.localScale = scale;

        PlayerPrefs.SetFloat("ScaleZ", scale.z);
        PlayerPrefs.Save();
    }
}
