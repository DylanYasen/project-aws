using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    [Header("UI")]
    public Transform handArea; // Parent object for displaying cards in the UI
    public GameObject cardPrefab;

    [Header("Data")]
    public CardDatabase cardDatabase;
    public List<Card> deck;
    public List<Card> hand;
    public List<Card> discardPile;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        InitStarterDeck();

        DrawCards(5);
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(0, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deck.Count == 0) return;

            Card drawnCard = deck[0];
            deck.RemoveAt(0);
            hand.Add(drawnCard);

            GameObject cardObject = Instantiate(cardPrefab, handArea);
            CardUI display = cardObject.GetComponent<CardUI>();
            display.Setup(drawnCard);
        }
    }

    public void InitStarterDeck()
    {
        AddCardToDeck("Instant Invoke", 2);

        AddCardToDeck("Cold Start Strike", 1);
        AddCardToDeck("Burst Scaling", 1);

        AddCardToDeck("Error Retry", 2);
        AddCardToDeck("Cloud Shield", 1);

        AddCardToDeck("Auto-Scaling", 1);
        AddCardToDeck("CloudWatch Logs", 1);

        AddCardToDeck("System Reboot", 1);
    }

    public void AddCardToDeck(string cardName, int count)
    {
        Card cardToAdd = cardDatabase.GetCardByName(cardName);

        if (cardToAdd == null)
        {
            Debug.LogError("Card not found in database: " + cardName);
            return;
        }

        for (int i = 0; i < count; i++)
        {
            deck.Add(cardToAdd);
        }
    }
}