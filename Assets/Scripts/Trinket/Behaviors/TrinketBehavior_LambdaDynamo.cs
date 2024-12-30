using UnityEngine;

[Tooltip("Start combat with 1 additional energy.")]
public class TrinketBehavior_LambdaDynamo : TrinketBehavior
{
    private void OnCombatStart()
    {
        Player.Instance.AddEnergy(1);
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