using UnityEngine;

public class HealEffect : CardEffect
{
    public int healAmount;
    public override void Execute(Unit source, Unit target)
    {
        if (target != null)
        {
            CombatManager.Instance.ApplyHealing(target, healAmount);
        }
    }
}