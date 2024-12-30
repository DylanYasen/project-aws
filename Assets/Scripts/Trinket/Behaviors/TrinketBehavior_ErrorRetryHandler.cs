using UnityEngine;

[Tooltip("Shuffle a random discarded card back into your deck each turn.")]
public class TrinketBehavior_ErrorRetryHandler : TrinketBehavior
{
    private void OnTurnStart()
    {
        if (DeckManager.Instance.discardPile.Count > 0)
        {
            int randomIndex = Random.Range(0, DeckManager.Instance.discardPile.Count);
            Card cardToShuffle = DeckManager.Instance.discardPile[randomIndex];
            DeckManager.Instance.discardPile.RemoveAt(randomIndex);
            DeckManager.Instance.AddCardToDeck(cardToShuffle);
        }
    }

    public override void OnAcquired()
    {
        TurnManager.Instance.OnPlayerTurnStart += OnTurnStart;
    }

    public override void OnRemoved()
    {
        TurnManager.Instance.OnPlayerTurnStart -= OnTurnStart;
    }
} 