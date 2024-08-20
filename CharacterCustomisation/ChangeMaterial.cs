using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private Material material1;
    [SerializeField] private Material material2;
    [SerializeField] private Material material3;

    [SerializeField] private Renderer shoeRenderer;
    [SerializeField] private Renderer sweaterRenderer;
    [SerializeField] private Renderer pantsRenderer;  

    private void Start()
    {
        SetMaterial(shoeRenderer, "ShoeMaterial", PlayerPrefs.GetInt("ShoeMaterial", 1));
        SetMaterial(sweaterRenderer, "SweaterMaterial", PlayerPrefs.GetInt("SweaterMaterial", 1));
        SetMaterial(pantsRenderer, "PantsMaterial", PlayerPrefs.GetInt("PantsMaterial", 1));
    }

    public void SetShoeMaterial(int index)
    {
        SetMaterial(shoeRenderer, "ShoeMaterial", index);
    }

    public void SetSweaterMaterial(int index)
    {
        SetMaterial(sweaterRenderer, "SweaterMaterial", index);
    }

    public void SetPantsMaterial(int index)
    {
        SetMaterial(pantsRenderer, "PantsMaterial", index);
    }

    private void SetMaterial(Renderer renderer, string key, int index)
    {
        switch (index)
        {
            case 1:
                renderer.material = material1;
                break;
            case 2:
                renderer.material = material2;
                break;
            case 3:
                renderer.material = material3;
                break;
        }

        PlayerPrefs.SetInt(key, index);
        PlayerPrefs.Save(); 
    }
}