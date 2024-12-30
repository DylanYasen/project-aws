using UnityEngine;

[Tooltip("Reduce the cost of a random card in your hand by 1 at the start of each turn.")]
public class TrinketBehavior_LatencyReducer : TrinketBehavior
{
    private void OnTurnStart()
    {
        if (DeckManager.Instance.hand.Count > 0)
        {
            int randomIndex = Random.Range(0, DeckManager.Instance.hand.Count);
            Card card = DeckManager.Instance.hand[randomIndex];
            
            // @todo: implement card cost reduction
            // card.ModifyCost(-1);
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