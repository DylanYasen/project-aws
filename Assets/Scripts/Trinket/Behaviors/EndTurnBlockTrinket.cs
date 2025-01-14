using UnityEngine;

[CreateAssetMenu(fileName = "End Turn Block Trinket", menuName = "Game/Trinket Behaviors/End Turn Block")]
public class EndTurnBlockTrinket : TrinketBehavior
{
    public int blockAmount = 1;

    public override void OnAcquired()
    {
        // Subscribe to turn end events from TurnManager instead of Player
        TurnManager.Instance.OnPlayerTurnEnd += HandleTurnEnd;
    }

    public override void OnRemoved()
    {
        // Unsubscribe from turn end events
        TurnManager.Instance.OnPlayerTurnEnd -= HandleTurnEnd;
    }

    private void HandleTurnEnd()
    {
        // Add block to player
        CombatManager.Instance.ApplyBlock(Player.Instance, blockAmount);
    }
} 