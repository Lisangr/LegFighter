using UnityEngine;
using UnityEngine.UI;

public class ForInputField : MonoBehaviour
{
    [SerializeField] private Text _textDisplay;
    [SerializeField] private InputField _inputField;

    private bool _isTextLocked;

    private void Awake()
    {
        _inputField.gameObject.SetActive(false);
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("TextLocked"))
        {
            _isTextLocked = PlayerPrefs.GetInt("TextLocked") == 1;
            _textDisplay.text = PlayerPrefs.GetString("TextValue", "Click to enter name");
        }
        else
        {
            _isTextLocked = false;
            _textDisplay.text = "Click to enter name";
        }

        _inputField.onEndEdit.AddListener(OnInputEndEdit);
    }

    public void OnTextClick()
    {
        if (_isTextLocked)
        {
            return; // Выход из функции, если текст уже заблокирован
        }

        _textDisplay.gameObject.SetActive(false);
        _inputField.gameObject.SetActive(true);
        _inputField.Select();
        _inputField.ActivateInputField();
    }

    public void OnInputEndEdit(string input)
    {
        if (!string.IsNullOrEmpty(input) && !_isTextLocked)
        {
            _textDisplay.text = input;
            _isTextLocked = true;

            PlayerPrefs.SetInt("TextLocked", 1);
            PlayerPrefs.SetString("TextValue", input);
        }

        _textDisplay.gameObject.SetActive(true);
        _inputField.gameObject.SetActive(false);
    }
}
