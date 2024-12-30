using UnityEngine;

[Tooltip("Deal 3 damage to all enemies when block is consumed.")]
public class TrinketBehavior_ReactiveDefense : TrinketBehavior
{
    private void OnBlockConsumed(Unit target, int amount)
    {
        if (target != Player.Instance) return;

        if (amount > 0)
        {
            foreach (var enemy in EncounterManager.Instance.enemies)
            {
                enemy.TakeDamage(3);
            }
        }
    }

    public override void OnAcquired()
    {
        CombatManager.Instance.OnBlockConsumed += OnBlockConsumed;
    }

    public override void OnRemoved()
    {
        CombatManager.Instance.OnBlockConsumed -= OnBlockConsumed;
    }
}