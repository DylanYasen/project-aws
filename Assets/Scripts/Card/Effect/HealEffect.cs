using UnityEngine;

public class HealEffect : CardEffect
{
    public int healAmount;

    public override void Execute(Unit source, Unit target)
    {
        Unit actualTarget = GetTarget(source, target);
        actualTarget.Heal(healAmount);
    }
}