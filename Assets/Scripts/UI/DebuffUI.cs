using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class DebuffUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TMP_Text durationText;

    public void SetEffect(StatusEffect effect)
    {
        icon.sprite = effect.icon;

        durationText.text = effect.duration > 1 ? effect.duration.ToString() : "";
    }

    public void ToggleVisibility(bool visible)
    {
        icon.gameObject.SetActive(visible);
        durationText.gameObject.SetActive(visible);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("buff hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("buff hover exit");
    }

}
