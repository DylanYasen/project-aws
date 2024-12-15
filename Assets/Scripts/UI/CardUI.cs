using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text cardNameText;
    public TMP_Text descriptionText;
    public Image cardArtImage;

    private Card card;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void Setup(Card cardData)
    {
        card = cardData;
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        if (card.cardArt != null)
        {
            cardArtImage.sprite = card.cardArt;
        }
    }

    public void OnCardClicked()
    {
        Debug.Log($"Playing card: {card.cardName}");
        // Handle card logic here (apply effect, deduct cost, etc.)
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // @todo: smooth this out with tweening
        transform.localScale = originalScale * 1.2f; // Enlarge card
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; // Reset scale
    }
}
