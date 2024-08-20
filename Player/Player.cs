using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public int currentHealth;
    public int maxHP;
    public static bool isEnemyDetected;

    [SerializeField] private float _moveSpeed = 20f;
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private int _vitalityToHP = 4;

    private Joystick _movingJoystick;
    private Rigidbody _rb;
    private Transform _player;
    private Animator _animator;
    private Transform _nearestEnemy;
    private List<Enemy> _enemiesInRange = new List<Enemy>();
    private int _vitality;
    private CinemachineVirtualCamera _mainCamera;

    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _movingJoystick = FindObjectOfType<Joystick>();
        isEnemyDetected = false;

        LoadVitality();
        CalculateMaxHP();
        currentHealth = maxHP;
    }

    void Update()
    {
        MoveWithJoystick();
        SearchForNearestEnemy();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetVitality(int vitality)
    {
        _vitality = vitality;
        CalculateMaxHP();
    }

    private void LoadVitality()
    {
        _vitality = PlayerPrefs.GetInt("Vitality", 25);
    }

    private void CalculateMaxHP()
    {
        maxHP = _vitality * _vitalityToHP;
        currentHealth = Mathf.Min(currentHealth, maxHP);
    }

    void MoveWithJoystick()
    {
        float horizontalInput = _movingJoystick.Horizontal;
        float verticalInput = _movingJoystick.Vertical;

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            RotateTowardsMovementDirection(moveDirection);
            _rb.MovePosition(transform.position + moveDirection * _moveSpeed * Time.deltaTime);
            _animator.SetTrigger("Run");
        }
        else
        {
            RotateTowardsEnemy();
            _animator.SetTrigger("Idle");
        }
    }

    void RotateTowardsMovementDirection(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        _player.rotation = Quaternion.Slerp(_player.rotation, targetRotation, Time.deltaTime * 10f);
    }

    void SearchForNearestEnemy()
    {
        _nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy != null && enemy.activeInHierarchy)
            {
                float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (currentDistance < nearestDistance && currentDistance <= _detectionRange)
                {
                    _nearestEnemy = enemy.transform;
                    nearestDistance = currentDistance;
                }
            }
        }
    }

    void RotateTowardsEnemy()
    {
        if (_nearestEnemy != null)
        {
            if (_nearestEnemy.gameObject.activeInHierarchy)
            {
                Vector3 directionToEnemy = (_nearestEnemy.position - transform.position).normalized;
                float targetAngle = Mathf.Atan2(directionToEnemy.x, directionToEnemy.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
                _player.rotation = Quaternion.Slerp(_player.rotation, targetRotation, Time.deltaTime * 5f);
            }
            else
            {
                SearchForNearestEnemy();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        int currentDefense = 10;
        float defenseReduction = currentDefense / 420f;
        int actualDamage = Mathf.RoundToInt(damage - damage * defenseReduction);
        currentHealth -= actualDamage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("Die");
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Time.timeScale = 0f;
    }

    public Transform GetNearestEnemy()
    {
        return _nearestEnemy;
    }

    public void OnEnemyDestroyed(GameObject enemy)
    {
        if (_nearestEnemy != null && _nearestEnemy.gameObject == enemy)
        {
            _nearestEnemy = null;
            SearchForNearestEnemy();
        }
    }
}