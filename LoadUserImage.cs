using UnityEngine;
using UnityEngine.UI;
using System.IO;

#if UNITY_EDITOR
using UnityEditor; 
#endif

public class LoadUserImage : MonoBehaviour
{
    public Image targetImage; 

    private string _imagePath;

    private void Awake()
    {
        _imagePath = Path.Combine(Application.persistentDataPath, "savedImage.png");

        LoadImageOnStart();
    }

    public void OnClickLoadImage()
    {
#if UNITY_EDITOR
        string path = EditorUtility.OpenFilePanel("Выберите изображение", "", "jpg,png");

        if (!string.IsNullOrEmpty(path))
        {
            LoadAndSaveImage(path);
        }
#else
        // For Android: call Native Gallery tochoise from gallery     PickImageFromGallery();
#endif
    }

    private void PickImageFromGallery()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                LoadAndSaveImage(path);
            }
        }, "Выберите изображение", "image/*");

        if (permission != NativeGallery.Permission.Granted)
        {
            Debug.Log("Разрешение на доступ к галерее не было предоставлено.");
        }
    }

    private void LoadAndSaveImage(string path)
    {
        byte[] imageData = File.ReadAllBytes(path);

        File.WriteAllBytes(_imagePath, imageData);

        SetImageFromPath(_imagePath);
    }

    private void LoadImageOnStart()
    {
        if (File.Exists(_imagePath))
        {
            SetImageFromPath(_imagePath);
        }
    }

    private void SetImageFromPath(string path)
    {
        byte[] imageData = File.ReadAllBytes(path);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        targetImage.sprite = newSprite;
    }
}

