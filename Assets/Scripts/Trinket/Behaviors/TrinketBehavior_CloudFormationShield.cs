using UnityEngine;

[Tooltip("Gain 5 block at the start of each combat.")]
public class TrinketBehavior_CloudFormationShield : TrinketBehavior
{
    private void OnCombatStart()
    {
        Player.Instance.AddBlock(5);
    }

    public override void OnAcquired()
    {
        TurnManager.Instance.OnCombatStart += OnCombatStart;
    }

    public override void OnRemoved()
    {
        TurnManager.Instance.OnCombatStart -= OnCombatStart;
    }
} 