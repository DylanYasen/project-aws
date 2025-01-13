using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class DebuffUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TMP_Text durationText;

    private StatusEffect effect;

    public void SetEffect(StatusEffect effect)
    {
        if (effect != null)
        {
            icon.sprite = effect.icon;
            durationText.text = effect.duration > 1 ? effect.duration.ToString() : "";
        }

        this.effect = effect;
    }

    public void ToggleVisibility(bool visible)
    {
        icon.gameObject.SetActive(visible);
        durationText.gameObject.SetActive(visible);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("buff hover");
        TooltipUI.Instance.DisplayTooltip(effect.effectName, effect.description, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("buff hover exit");
        TooltipUI.Instance.Hide();
    }

}
