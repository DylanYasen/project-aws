using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    [Header("Data")]
    public CardDatabase cardDatabase;
    public List<Card> deck;
    public List<Card> hand;
    public List<Card> discardPile;

    public event System.Action<Card> OnCardPlayed;
    public event System.Action<Card> OnCardDrawn;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void Init()
    {
        InitStarterDeck();
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
            if (deck.Count == 0)
            {
                ReshuffleDiscardPile();
            }

            Assert.IsTrue(deck.Count > 0, "Deck is empty, cannot draw cards.");

            Card drawnCard = deck[0];
            deck.RemoveAt(0);
            hand.Add(drawnCard);


            // @todo: pool these
            var handArea = CombatSceneUIReferences.Instance.handArea;
            var cardPrefab = CombatSceneUIReferences.Instance.cardPrefab;
            GameObject cardObject = Instantiate(cardPrefab, handArea);
            CardUI cardUI = cardObject.GetComponent<CardUI>();
            cardUI.Setup(drawnCard);

            OnCardDrawn?.Invoke(drawnCard);
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

    public void AddCardToDeck(Card cardToAdd)
    {
        if (cardToAdd == null)
        {
            Debug.LogError("Cannot add null card to deck");
            return;
        }

        deck.Add(cardToAdd);
    }

    public void PlayCard(Card card)
    {
        OnCardPlayed?.Invoke(card);

        hand.Remove(card);
        discardPile.Add(card);
        Debug.Log($"Card {card.cardName} discarded.");
    }

    private void ReshuffleDiscardPile()
    {
        deck.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();

        Debug.Log("Discard pile reshuffled into the deck.");
    }
}