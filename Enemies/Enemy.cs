using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int _currentHealth;

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] Image _healthBar;
    [SerializeField] private CinemachineVirtualCamera _deathCamera;
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private float _attackCooldown = 1.5f; // Добавляем время между атаками

    private int _maxHealth;
    private Player _player;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private float _lastAttackTime; // Хранит время последней атаки

    public delegate void DeathAction(int exp);
    public static event DeathAction OnEnemyDeath;
    public delegate void LootAction();
    public static event LootAction OnEnemyDestroy;

    void Start()
    {
        _maxHealth = _enemyData.health;
        _currentHealth = _maxHealth;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;

        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();

        UpdateHealthDisplay();
    }

    private void Update()
    {
        if (_player != null)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            AnimateMovement();
        }

        if (_currentHealth <= _maxHealth * 0.3)
        {
            _deathCamera.gameObject.SetActive(true);
        }

        UpdateHealthDisplay();
    }

    private void AnimateMovement()
    {
        Vector3 velocity = _navMeshAgent.velocity;
        if (velocity.magnitude > 0.1f)
        {
            _animator.SetTrigger("Run");
        }
        else
        {
            _animator.SetTrigger("Idle");
        }
    }

    public void Initialize()
    {
        _healthBar.fillAmount = 1f;
        _currentHealth = _enemyData.health;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;
        UpdateHealthDisplay();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        UpdateHealthDisplay();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthDisplay()
    {
        _healthBar.fillAmount = (float)_currentHealth / _maxHealth;
    }

    private void Die()
    {
        int exp = _enemyData.exp;
        OnEnemyDeath?.Invoke(exp);
        OnEnemyDestroy?.Invoke();
        _deathCamera.gameObject.SetActive(false);

        _currentHealth = _maxHealth;
        _navMeshAgent.speed = _moveSpeed;
        EnemyPool.Instance.ReturnEnemy(this);
    }

    public void AttackPlayer()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown) 
        {
            _player = FindObjectOfType<Player>();

            if (_player != null)
            {
                int randomAttack = Random.Range(0, 3);

                switch (randomAttack)
                {
                    case 0:
                        TriggerKnee();
                        break;
                    case 1:
                        TriggerLeg();
                        break;
                    case 2:
                        TriggerHand();
                        break;
                }

                _lastAttackTime = Time.time; // Обновляем время последней атаки
            }
        }
    }

    private void TriggerKnee()
    {
        _animator.SetTrigger("Knee");
        _player.TakeDamage(_enemyData.damageKnee);
    }

    private void TriggerLeg()
    {
        _animator.SetTrigger("Leg");
        _player.TakeDamage(_enemyData.damageLeg);
    }

    private void TriggerHand()
    {
        _animator.SetTrigger("KneeAndLeg");
        _player.TakeDamage(_enemyData.damageKneeAndLeg);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _navMeshAgent.speed = 0f;
            _animator.SetTrigger("Idle");
            AttackPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _navMeshAgent.speed = _moveSpeed;
            _animator.SetTrigger("Run");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _navMeshAgent.speed = 0f;
            AttackPlayer();
        }
    }
}


/*
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int _currentHealth;

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] Image _healthBar;
    [SerializeField] private CinemachineVirtualCamera _deathCamera;
    [SerializeField] private float _moveSpeed = 3.5f;

    private int _maxHealth;
    private Player _player;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;  // Добавляем компонент Animator

    public delegate void DeathAction(int exp);
    public static event DeathAction OnEnemyDeath;
    public delegate void LootAction();
    public static event LootAction OnEnemyDestroy;

    void Start()
    {
        _maxHealth = _enemyData.health;
        _currentHealth = _maxHealth;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;

        _animator = GetComponent<Animator>();  // Инициализируем Animator
        _player = FindObjectOfType<Player>();

        UpdateHealthDisplay();
    }

    private void Update()
    {
        if (_player != null)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            AnimateMovement();  // Добавляем анимацию движения
        }

        if (_currentHealth <= _maxHealth * 0.3)
        {
            _deathCamera.gameObject.SetActive(true);
        }

        UpdateHealthDisplay();
    }

    private void AnimateMovement()
    {
        Vector3 velocity = _navMeshAgent.velocity;
        if (velocity.magnitude > 0.1f)
        {
            // Используем те же параметры анимации, что и в скрипте игрока
            _animator.SetTrigger("Run");
        }
        else
        {
            _animator.SetTrigger("Idle");
        }
    }

    public void Initialize()
    {
        _healthBar.fillAmount = 1f;
        _currentHealth = _enemyData.health;
        UpdateHealthDisplay();

        Debug.Log($"Enemy initialized: {gameObject.name} at position {transform.position}");
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        _currentHealth = Mathf.Max(_currentHealth, 0);
        UpdateHealthDisplay();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthDisplay()
    {
        _healthBar.fillAmount = (float)_currentHealth / _maxHealth;
    }

    private void Die()
    {
        int exp = _enemyData.exp;
        OnEnemyDeath?.Invoke(exp);
        OnEnemyDestroy?.Invoke();
        _deathCamera.gameObject.SetActive(false);

        _currentHealth = _maxHealth;
        EnemyPool.Instance.ReturnEnemy(this);
    }

    public void AttackPlayer()
    {
        _player = FindObjectOfType<Player>();

        if (_player != null)
        {
            int randomAttack = Random.Range(0, 3);  // Генерируем случайное число от 0 до 2

            switch (randomAttack)
            {
                case 0:
                    TriggerKnee();
                    break;
                case 1:
                    TriggerLeg();
                    break;
                case 2:
                    TriggerHand();
                    break;
            }
        }
    }

    private void TriggerKnee()
    {
        _animator.SetTrigger("Knee");
        _player.TakeDamage(_enemyData.damageKnee);
    }

    private void TriggerLeg()
    {
        _animator.SetTrigger("Leg");
        _player.TakeDamage(_enemyData.damageLeg);
    }

    private void TriggerHand()
    {
        _animator.SetTrigger("KneeAndLeg");
        _player.TakeDamage(_enemyData.damageKneeAndLeg);
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _navMeshAgent.speed = 0f;
            _animator.SetTrigger("Idle");
            AttackPlayer();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _navMeshAgent.speed = _moveSpeed;
            _animator.SetTrigger("Run");
            AttackPlayer();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _navMeshAgent.speed = 0f;
            AttackPlayer();
        }
    }
}
*/