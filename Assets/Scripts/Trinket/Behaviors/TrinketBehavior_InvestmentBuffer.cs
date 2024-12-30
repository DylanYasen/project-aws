using UnityEngine;

[Tooltip("Gain 10 gold at the start of combat but lose 5 gold if you end with unspent energy each turn.")]
public class TrinketBehavior_InvestmentBuffer : TrinketBehavior
{
    private void OnCombatStart()
    {
        GameManager.Instance.AddGold(10);
    }

    private void OnTurnEnd()
    {
        if (Player.Instance.currentEnergy > 0)
        {
            GameManager.Instance.SpendGold(5);
        }
    }

    public override void OnAcquired()
    {
        TurnManager.Instance.OnCombatStart += OnCombatStart;
        TurnManager.Instance.OnPlayerTurnEnd += OnTurnEnd;
    }

    public override void OnRemoved()
    {
        TurnManager.Instance.OnCombatStart -= OnCombatStart;
        TurnManager.Instance.OnPlayerTurnEnd -= OnTurnEnd;
    }
} 