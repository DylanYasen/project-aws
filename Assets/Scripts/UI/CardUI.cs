using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;
    public TMP_Text descriptionText;
    public Image cardArtImage;

    private Card card;

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
}
