using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour
{
    public TMP_Text cardNameText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;
    public Image cardArtImage;
    public Button purchaseButton;
    public Card card;


    public void Initialize(Card card, Action<ShopCardUI> purchaseCallback)
    {
        this.card = card;
        cardNameText.text = card.name;
        descriptionText.text = card.description;
        priceText.text = card.price.ToString();
        cardArtImage.sprite = card.cardArt;

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(() => purchaseCallback?.Invoke(this));
    }

    public void Purchased()
    {
        purchaseButton.interactable = false;
        priceText.text = "Purchased";
    }
}