using UnityEngine;

[Tooltip("Reduce the cost of the first card you play each turn by 1 energy.")]
public class TrinketBehavior_CardAccelerator : TrinketBehavior
{
    private bool hasReducedThisTurn = false;

    private void OnCardPlayed(Card card)
    {
        if (!hasReducedThisTurn)
        {
            // @todo: implement card cost reduction
            
            hasReducedThisTurn = true;
        }
    }

    private void OnTurnStart()
    {
        hasReducedThisTurn = false;
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