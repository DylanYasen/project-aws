using System;
using UnityEngine;

[Tooltip("Each time you take damage, draw 1 card.")]
public class TrinketBehavior_ErrorLogger : TrinketBehavior
{
    private void OnDamageTaken(Unit unit, int damage)
    {
        if (unit != Player.Instance) return;
        if (damage > 0)
        {
            DeckManager.Instance.DrawCards(1);
        }
    }

    public override void OnAcquired()
    {
        CombatManager.Instance.OnDamageTaken += OnDamageTaken;
    }

    public override void OnRemoved()
    {
        CombatManager.Instance.OnDamageTaken -= OnDamageTaken;
    }
}