using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundScaler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found!");
            return;
        }

        ScaleToFitHeight();
    }

    private void ScaleToFitHeight()
    {
        // Get the world height of the camera view
        float cameraHeight = mainCamera.orthographicSize * 2f;
        
        // Get the sprite's original size
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        
        // Calculate the scale needed to match camera height
        float scale = cameraHeight / spriteSize.y;
        Debug.Log($"Scale: {scale}");

        // Apply the scale while maintaining aspect ratio
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    // Optional: Add this if you want to update when the game view is resized
    private void OnValidate()
    {
        if (spriteRenderer == null || mainCamera == null) return;
        ScaleToFitHeight();
    }

    private void OnEnable()
    {
        if (mainCamera == null) return;
        
        // Print camera dimensions in world units
        float worldHeight = mainCamera.orthographicSize * 2;
        float worldWidth = worldHeight * mainCamera.aspect;
        Debug.Log($"Camera world dimensions: {worldWidth} x {worldHeight} units");
    }
} 