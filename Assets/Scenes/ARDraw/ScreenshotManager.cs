using System.Collections;
using System.IO;
using UnityEngine;
using NativeGalleryNamespace; // Add this line to use Native Gallery

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // Wait for end of frame to ensure everything is rendered
        yield return new WaitForEndOfFrame();

        // Create a texture to hold the screenshot
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Rect captureRect = new Rect(0, 0, Screen.width, Screen.height);
        screenTexture.ReadPixels(captureRect, 0, 0);
        screenTexture.Apply();

        // Save the screenshot to the Photos app using Native Gallery
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(screenTexture, "MyGallery", "Screenshot.png", (success, path) => {
            Debug.Log("Media save result: " + success + " " + path);
        });

        Debug.Log("Permission result: " + permission);

        // To avoid memory leaks
        Destroy(screenTexture);
    }
}
