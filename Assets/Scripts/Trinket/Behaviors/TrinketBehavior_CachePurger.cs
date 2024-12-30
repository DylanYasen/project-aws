using UnityEngine;

[Tooltip("Remove all debuffs at the start of your turn.")]
public class TrinketBehavior_CachePurger : TrinketBehavior
{
    private void OnTurnStart()
    {
        // @todo: implement debuff
        // Player.Instance.RemoveAllDebuffs();
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