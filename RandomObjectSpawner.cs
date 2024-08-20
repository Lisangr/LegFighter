using UnityEngine;
using UnityEngine.AI;

public class RandomObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private int _numberOfObjects = 6;
    [SerializeField] private NavMeshSurface _navMeshSurface; 

    void Start()
    {
        SpawnObjects();
        BakeNavMesh(); 
    }

    void SpawnObjects()
    {
        Bounds groundBounds = _ground.GetComponent<Collider>().bounds;

        for (int i = 0; i < _numberOfObjects; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(groundBounds.min.x, groundBounds.max.x),
                groundBounds.min.y + 0.5f, 
                Random.Range(groundBounds.min.z, groundBounds.max.z)
            );

            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }

    void BakeNavMesh()
    {
        if (_navMeshSurface != null)
        {
            _navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.LogWarning("NavMeshSurface не установлена!");
        }
    }
}
