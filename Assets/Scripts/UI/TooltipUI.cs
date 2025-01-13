using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    private static TooltipUI instance;
    public static TooltipUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TooltipUI>();
                if (instance == null)
                {
                    Debug.LogError("No TooltipUI found in scene!");
                }
            }
            return instance;
        }
    }

    [Header("References")]
    [SerializeField] private RectTransform containerRect;
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private LayoutElement layoutElement;
    
    [Header("Settings")]
    [SerializeField] private float maxWidth = 400f;
    [SerializeField] private float maxHeight = 300f;
    [SerializeField] private float padding = 10f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        Hide();
    }

    public void DisplayTooltip(string title, string text, Vector2 position)
    {
        gameObject.SetActive(true);
        titleText.text = title;
        tooltipText.text = text;

        // Force layout update
        LayoutRebuilder.ForceRebuildLayoutImmediate(containerRect);
        
        // Get preferred sizes - consider both title and content
        float preferredWidth = Mathf.Max(
            titleText.preferredWidth,
            tooltipText.preferredWidth
        ) + (padding * 2);
        
        float preferredHeight = titleText.preferredHeight + 
                               tooltipText.preferredHeight + 
                               (padding * 2);

        // Apply size constraints
        float width = Mathf.Min(preferredWidth, maxWidth);
        float height = Mathf.Min(preferredHeight, maxHeight);

        // Update container size
        containerRect.sizeDelta = new Vector2(width, height);

        // Position tooltip
        SetPosition(position);
    }

    private void SetPosition(Vector2 position)
    {
        // Convert screen position to local position
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            position,
            null,
            out localPoint
        );

        containerRect.anchoredPosition = localPoint;

        // Keep tooltip on screen
        Vector2 size = containerRect.sizeDelta;
        Vector2 screenBounds = new Vector2(Screen.width, Screen.height);
        
        if (localPoint.x + size.x > screenBounds.x)
            localPoint.x = screenBounds.x - size.x;
        if (localPoint.y + size.y > screenBounds.y)
            localPoint.y = screenBounds.y - size.y;
        if (localPoint.x < 0)
            localPoint.x = 0;
        if (localPoint.y < 0)
            localPoint.y = 0;

        containerRect.anchoredPosition = localPoint;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
} 