using UnityEngine;

[System.Serializable]
public class MaterialSet
{
    public Material[] shoeMaterials = new Material[3];    // Массив материалов для обуви
    public Material[] sweaterMaterials = new Material[3]; // Массив материалов для свитера
    public Material[] pantsMaterials = new Material[3];   // Массив материалов для штанов
}

public class MaterialApplier
{
    private Renderer _shoeRenderer;
    private Renderer _sweaterRenderer;
    private Renderer _pantsRenderer;

    public MaterialApplier(Renderer shoeRenderer, Renderer sweaterRenderer, Renderer pantsRenderer)
    {
        _shoeRenderer = shoeRenderer;
        _sweaterRenderer = sweaterRenderer;
        _pantsRenderer = pantsRenderer;
    }

    public void ApplyMaterials(MaterialSet materialSet, int shoeMaterialIndex, int sweaterMaterialIndex, int pantsMaterialIndex)
    {
        _shoeRenderer.material = materialSet.shoeMaterials[shoeMaterialIndex];
        _sweaterRenderer.material = materialSet.sweaterMaterials[sweaterMaterialIndex];
        _pantsRenderer.material = materialSet.pantsMaterials[pantsMaterialIndex];
    }
}

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;     // Префаб игрока
    [SerializeField] private Transform _spawnPoint;        // Точка спавна
    [SerializeField] private MaterialSet _materialSet;     // Сет материалов

    void Start()
    {
        GameObject player = Instantiate(_playerPrefab, _spawnPoint.position, _spawnPoint.rotation);

        ApplyScale(player);
        ApplyMaterials(player);
    }

    private void ApplyScale(GameObject player)
    {
        float savedScaleX = PlayerPrefs.GetFloat("ScaleX", 300f);
        float savedScaleY = PlayerPrefs.GetFloat("ScaleY", 300f);
        float savedScaleZ = PlayerPrefs.GetFloat("ScaleZ", 300f);
        float scaleX = savedScaleX / 100;
        float scaleY = savedScaleY / 100;
        float scaleZ = savedScaleZ / 100;
        
        player.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    private void ApplyMaterials(GameObject player)
    {
        Renderer shoeRenderer = player.transform.Find("Boots3").GetComponent<Renderer>();
        Renderer sweaterRenderer = player.transform.Find("Shirt3").GetComponent<Renderer>();
        Renderer pantsRenderer = player.transform.Find("Pants2").GetComponent<Renderer>();

        if (shoeRenderer != null && sweaterRenderer != null && pantsRenderer != null)
        {
            int shoeMaterialIndex = PlayerPrefs.GetInt("ShoeMaterial", 1) - 1;
            int sweaterMaterialIndex = PlayerPrefs.GetInt("SweaterMaterial", 1) - 1;
            int pantsMaterialIndex = PlayerPrefs.GetInt("PantsMaterial", 1) - 1;

            MaterialApplier materialApplier = new MaterialApplier(shoeRenderer, sweaterRenderer, pantsRenderer);
            materialApplier.ApplyMaterials(_materialSet, shoeMaterialIndex, sweaterMaterialIndex, pantsMaterialIndex);
        }
        else
        {
            Debug.LogError("One or more renderers not found in the player prefab.");
        }
    }
}
