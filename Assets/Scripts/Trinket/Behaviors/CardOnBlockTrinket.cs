using UnityEngine;

[CreateAssetMenu(fileName = "Card On Block Trinket", menuName = "Game/Trinket Behaviors/Card On Block")]
public class CardOnBlockTrinket : TrinketBehavior
{
    public int drawCardCount = 1;

    public override void OnAcquired()
    {
        // Subscribe to block consumed events from CombatManager
        CombatManager.Instance.OnBlockConsumed += HandleBlockConsumed;
    }

    public override void OnRemoved()
    {
        // Unsubscribe from block consumed events
        CombatManager.Instance.OnBlockConsumed -= HandleBlockConsumed;
    }

    private void HandleBlockConsumed(Unit unit, int blockAmount)
    {
        // Only trigger for player
        if (unit == Player.Instance)
        {
            Debug.Log("[Card On Block Trinket] Drawing " + drawCardCount + " cards");
            DeckManager.Instance.DrawCards(drawCardCount);
        }
    }
} 