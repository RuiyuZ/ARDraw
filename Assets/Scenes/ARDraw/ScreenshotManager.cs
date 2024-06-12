using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using NativeGalleryNamespace;

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager Instance { get; private set; }
    public GameObject flashPanel;

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

        // Show the flash effect after taking the screenshot
        StartCoroutine(FlashEffect());

        // To avoid memory leaks
        Destroy(screenTexture);
    }

    private IEnumerator FlashEffect()
    {
        // Enable the flash panel
        flashPanel.SetActive(true);

        // Get the panel's image component
        Image flashImage = flashPanel.GetComponent<Image>();

        // Set the initial color (fully opaque)
        flashImage.color = new Color(1, 1, 1, 1);

        // Animate the flash effect (fade out)
        float duration = 0.5f; // Duration of the flash effect
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsed / duration);
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // Ensure the panel is fully transparent at the end
        flashImage.color = new Color(1, 1, 1, 0);

        // Disable the flash panel
        flashPanel.SetActive(false);
    }
}
