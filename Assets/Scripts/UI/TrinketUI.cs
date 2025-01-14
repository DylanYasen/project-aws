using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TrinketUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image trinketIcon;

    private Trinket trinket;

    private void Awake()
    {
        trinketIcon = GetComponent<Image>();
    }

    public void Initialize(Trinket trinket)
    {
        this.trinket = trinket;
        
        if (trinketIcon != null)
        {
            trinketIcon.sprite = trinket.icon;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipUI.Instance.DisplayTooltip(trinket.name, trinket.description, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance.Hide();
    }
} 