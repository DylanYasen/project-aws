using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DeckManager
{
    public static DeckManager Instance { get; private set; }

    public List<Card> allCards = new();

    private Dictionary<string, Card> allCardsByName = new();
    public List<Card> deck = new();
    public List<Card> hand = new();
    public List<Card> discardPile = new();

    public event System.Action<Card> OnCardPlayed;
    public event System.Action<Card> OnCardDrawn;

    public DeckManager()
    {
        Instance = this;
    }

    public void InitForCombat()
    {
        if (this.deck.Count < 1)
        {
            InitStarterDeck();
        }
        else
        {
            this.deck.AddRange(discardPile);
            this.deck.AddRange(hand);
            this.discardPile.Clear();
            this.hand.Clear();
        }
        ShuffleDeck();
    }

    public async void LoadResources()
    {
        var cardLoadHandle = Addressables.LoadAssetsAsync<Card>("Cards", card =>
        {
            Debug.Log($"Loaded card: {card.cardName}");
            allCards.Add(card);
            allCardsByName[card.cardName] = card;
        });

        await cardLoadHandle.Task;

        if (cardLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successfully loaded all cards");
        }
        else
        {
            Debug.LogError("Failed to load cards");
        }
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
            GameObject cardObject = GameObject.Instantiate(cardPrefab, handArea);
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
        if (!allCardsByName.TryGetValue(cardName, out Card cardToAdd))
        {
            Debug.LogError("Card not found in database: " + cardName);
            return;
        }

        for (int i = 0; i < count; i++)
        {
            var cardInstance = Object.Instantiate(cardToAdd);
            deck.Add(cardInstance);
        }
    }

    public void AddCardToDeck(Card cardToAdd)
    {
        if (cardToAdd == null)
        {
            Debug.LogError("Cannot add null card to deck");
            return;
        }

        var cardInstance = Object.Instantiate(cardToAdd);
        deck.Add(cardInstance);
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