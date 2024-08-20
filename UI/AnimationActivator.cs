using System.Collections;
using UnityEngine;

public class AnimationActivator : MonoBehaviour
{
    [SerializeField] private int _damageKnee;
    [SerializeField] private int _damageFly;
    [SerializeField] private int _damageLeg;
    [SerializeField] private int _damageKneeAndLeg;

    private Animator _playerAnimator;
    private Player _player;
    void Start()
    {
        StartCoroutine(DelayedPlayerSearch());
    }

    private IEnumerator DelayedPlayerSearch()
    {
        yield return new WaitForSeconds(1.0f);

        _player = FindObjectOfType<Player>();

        if (_player != null)
        {
            _playerAnimator = _player.GetComponent<Animator>();
        }
    }

    public void TriggerKnee()
    {
        SetTrigger("Knee");
        DamageNearestEnemy(_damageKnee);
    }

    public void TriggerFly()
    {
        if (_player == null)
            return;

        // ���������� ����������� ����� �� ������
        Vector3 backwardDirection = -_player.transform.forward;

        // ���������� ��������� ����� � ����� ���� (2 �����)
        Vector3 origin = _player.transform.position;
        float rayDistance = 2.0f;

        // ��������� Raycast �����
        if (!Physics.Raycast(origin, backwardDirection, out RaycastHit hit, rayDistance))
        {
            // ���� ������ �� �������, ���������� ������ �����
            _player.transform.position += backwardDirection * rayDistance;
        }
        else
        {
            // ���������, ���� ��������� ������ � ����� 'wall'
            if (hit.collider.tag != "wall")
            {
                _player.transform.position += backwardDirection * rayDistance;
            }
        }
    }

    public void TriggerLeg()
    {
        SetTrigger("Leg");
        DamageNearestEnemy(_damageLeg);
    }

    public void TriggerHand()
    {
        SetTrigger("KneeAndLeg");
        DamageNearestEnemy(_damageKneeAndLeg);
    }
    
    private void DamageNearestEnemy(int damage)
    {
        if (_player != null && _player.GetNearestEnemy() != null)
        {
            Enemy nearestEnemy = _player.GetNearestEnemy().GetComponent<Enemy>();
            if (nearestEnemy != null)
            {
                nearestEnemy.TakeDamage(damage);
            }
        }
    }
    
    private void SetTrigger(string triggerName)
    {
        if (_playerAnimator != null)
        {
            _playerAnimator.SetTrigger(triggerName);
        }
    }
}
