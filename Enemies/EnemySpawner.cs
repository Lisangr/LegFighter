using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int _maxEnemies = 3;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval && CountActiveEnemies() < _maxEnemies)
        {
            SpawnEnemy();
            _timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        Enemy enemy = EnemyPool.Instance.GetEnemy();
        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;
    }

    private int CountActiveEnemies()
    {
        return FindObjectsOfType<Enemy>().Length;
    }
}
