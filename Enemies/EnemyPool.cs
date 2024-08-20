using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int poolSize = 3;

    private Queue<Enemy> enemyPool = new Queue<Enemy>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.gameObject.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    public Enemy GetEnemy()
    {
        if (enemyPool.Count > 0)
        {
            Enemy enemy = enemyPool.Dequeue();
            enemy.gameObject.SetActive(true);
            return enemy;
        }
        else
        {
            Enemy enemy = Instantiate(enemyPrefab);
            return enemy;
        }
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
