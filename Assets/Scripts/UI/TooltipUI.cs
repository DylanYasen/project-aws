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
        // offset based on size so its not on top of the mouse position
        Vector2 offset = new Vector2(width, 0);
        containerRect.position = position + offset;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
} 