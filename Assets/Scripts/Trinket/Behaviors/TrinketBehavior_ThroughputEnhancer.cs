using UnityEngine;

[Tooltip("Gain 1 energy every third card you play in a turn.")]
public class TrinketBehavior_ThroughputEnhancer : TrinketBehavior
{
    private int cardsPlayedThisTurn = 0;

    private void OnCardPlayed(Card card)
    {
        cardsPlayedThisTurn++;
        if (cardsPlayedThisTurn % 3 == 0)
        {
            Player.Instance.AddEnergy(1);
        }
    }

    private void OnTurnStart()
    {
        cardsPlayedThisTurn = 0;
    }

    public override void OnAcquired()
    {
        DeckManager.Instance.OnCardPlayed += OnCardPlayed;
        TurnManager.Instance.OnPlayerTurnStart += OnTurnStart;
    }

    public override void OnRemoved()
    {
        DeckManager.Instance.OnCardPlayed -= OnCardPlayed;
        TurnManager.Instance.OnPlayerTurnStart -= OnTurnStart;
    }
} 