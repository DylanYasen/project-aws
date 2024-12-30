using System;
using UnityEngine;

[Tooltip("If an enemy attack exceeds 10 damage, reduce it by 3.")]
public class TrinketBehavior_ThrottlingResolver : TrinketBehavior
{
    private void OnBeingAttacked(Unit unit, ref int damage)
    {
        if (damage > 10)
        {
            damage -= 3;
        }
    }

    public override void OnAcquired()
    {
        // CombatManager.Instance.OnDamageAppliedPreBlock += OnBeingAttacked;

    }


    public override void OnRemoved()
    {
        // CombatManager.Instance.OnDamageAppliedPreBlock -= OnBeingAttacked;
    }
}