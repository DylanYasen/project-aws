using UnityEngine;

[Tooltip("Gain 1 block for every unspent energy at the end of your turn.")]
public class TrinketBehavior_OverflowCircuit : TrinketBehavior
{
    private void OnTurnEnd()
    {
        Player.Instance.AddBlock(Player.Instance.currentEnergy);
    }

    public override void OnAcquired()
    {
        TurnManager.Instance.OnPlayerTurnEnd += OnTurnEnd;
    }

    public override void OnRemoved()
    {
        TurnManager.Instance.OnPlayerTurnEnd -= OnTurnEnd;
    }
}