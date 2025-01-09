using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopTrinketUI : MonoBehaviour
{
    [SerializeField] private Image trinketIcon;
    [SerializeField] private TMP_Text trinketNameText;
    [SerializeField] private TMP_Text trinketDescriptionText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button purchaseButton;

    public Trinket trinket;

    public void Initialize(Trinket trinket, Action<ShopTrinketUI> purchaseCallback)
    {
        this.trinket = trinket;
        trinketNameText.text = trinket.name;
        trinketDescriptionText.text = trinket.description;
        trinketIcon.sprite = trinket.icon;
        priceText.text = trinket.price.ToString();

        // price = itemPrice;
        // TODO: Set actual trinket data
        // trinketNameText.text = "Sample Trinket";
        // trinketDescriptionText.text = "This is a sample description";
        // priceText.text = $"{price} Gold";
        purchaseButton.onClick.AddListener(() => purchaseCallback?.Invoke(this));
    }

    public void SetSold()
    {
        purchaseButton.interactable = false;
        priceText.text = "SOLD";
    }
}