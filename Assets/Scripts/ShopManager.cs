using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int cardCount = 4;
    public int trinketCount = 4;

    public List<Card> cards;
    public List<Trinket> trinkets;

    ShopCardUI[] shopCardUIs;
    ShopTrinketUI[] shopTrinketUIs;

    void Start()
    {
        shopCardUIs = transform.GetComponentsInChildren<ShopCardUI>();
        shopTrinketUIs = transform.GetComponentsInChildren<ShopTrinketUI>();

        GenerateShopItems();
        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        for (int i = 0; i < cardCount; i++)
        {
            shopCardUIs[i].Initialize(cards[i], PurchaseCard);
        }

        for (int i = 0; i < trinketCount; i++)
        {
            shopTrinketUIs[i].Initialize(trinkets[i], PurchaseTrinket);
        }
    }

    public void PurchaseCard(ShopCardUI cardUI)
    {
        if (GameManager.Instance.SpendGold(cardUI.card.price))
        {
            Debug.Log($"Purchased {cardUI.card.name} for {cardUI.card.price} gold.");
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    public void PurchaseTrinket(ShopTrinketUI trinketUI)
    {
        if (GameManager.Instance.SpendGold(trinketUI.trinket.price))
        {
            Debug.Log($"Purchased {trinketUI.trinket.name} for {trinketUI.trinket.price} gold.");
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    private void GenerateShopItems()
    {
        cards = new List<Card>(cardCount);
        var allCards = DeckManager.Instance.allCards;
        int totalCardCount = allCards.Count;
        if (totalCardCount > 0)
        {
            // Fisher-Yates shuffle first n elements
            for (int i = 0; i < cardCount && i < totalCardCount; i++)
            {
                int randomIndex = Random.Range(i, totalCardCount);
                cards.Add(allCards[randomIndex]);

                // Swap elements to avoid duplicates
                if (randomIndex != i)
                {
                    var temp = allCards[i];
                    allCards[i] = allCards[randomIndex];
                    allCards[randomIndex] = temp;
                }
            }
        }


        trinkets = new List<Trinket>(trinketCount);
        HashSet<string> generatedTrinketNames = new HashSet<string>();
        foreach (var playerTrinket in TrinketManager.Instance.playerTrinkets)
        {
            // Add player owned trinkets to avoid duplicates
            generatedTrinketNames.Add(playerTrinket.name);
        }

        int attempts = 0;
        int maxAttempts = trinketCount * 3; // Prevent infinite loops

        while (trinkets.Count < trinketCount && attempts < maxAttempts)
        {
            var trinket = TrinketManager.Instance.GetRandomTrinket();
            attempts++;

            if (trinket != null && !generatedTrinketNames.Contains(trinket.name))
            {
                trinkets.Add(trinket);
                generatedTrinketNames.Add(trinket.name);
            }
            else if (trinket == null)
            {
                Debug.LogWarning("Failed to generate trinket for shop");
            }
        }

        if (trinkets.Count < trinketCount)
        {
            Debug.LogWarning($"Could only generate {trinkets.Count} unique trinkets for shop after {attempts} attempts");
        }
    }
}
