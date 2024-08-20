using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoFight : MonoBehaviour
{   
    [SerializeField] private float _checkInterval = 0.1f; 
    [SerializeField] private Image _image;
    [SerializeField] private float _flyInterval = 5.0f;
    [SerializeField] private AnimationActivator _animationActivator;
    private Animator _animator;    
    private bool _isAutoFighting = false; 
    private string[] triggers = { "Knee", "Leg", "KneeAndLeg" }; 
    private int _currentTriggerIndex = 0; 

    void Start()
    {
        _animationActivator = GetComponent<AnimationActivator>();
        StartCoroutine(DelayedPlayerSearch());
        StartCoroutine(AutoFightCoroutine());
        StartCoroutine(FlyCoroutine());
        UpdateButtonColor();
    }

    private IEnumerator DelayedPlayerSearch()
    {
        yield return new WaitForSeconds(1.0f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            _animator = player.GetComponent<Animator>();            
        }
    }
    public void ToggleAutoFight()
    {
        _isAutoFighting = !_isAutoFighting; 
        UpdateButtonColor();
    }
    private void UpdateButtonColor()
    {
        _image.color = _isAutoFighting ? Color.green : Color.red;
    }

    private IEnumerator AutoFightCoroutine()
    {
        while (true)
        {
            if (_isAutoFighting)
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    ExecuteTrigger(triggers[_currentTriggerIndex]);

                    _currentTriggerIndex = (_currentTriggerIndex + 1) % triggers.Length;

                    yield return new WaitUntil(() => !_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
                }
            }
            yield return new WaitForSeconds(_checkInterval);
        }
    }

    private IEnumerator FlyCoroutine()
    {
        while (true)
        {
            if (_isAutoFighting)
            {
                _animationActivator.TriggerFly();
            }
            yield return new WaitForSeconds(_flyInterval);
        }
    }

    private void ExecuteTrigger(string trigger)
    {
        switch (trigger)
        {
            case "Knee":
                _animationActivator.TriggerKnee();
                break;
            case "Leg":
                _animationActivator.TriggerLeg();
                break;
            case "KneeAndLeg":
                _animationActivator.TriggerHand();
                break;
            default:
                Debug.LogError("Unknown trigger: " + trigger);
                break;
        }
    }
}