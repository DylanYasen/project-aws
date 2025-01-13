using UnityEngine;

public class ApplyStatusEffect : CardEffect
{
    [SerializeReference]
    public StatusEffect statusEffect;

    public override void Execute(Unit source, Unit target)
    {
        Unit actualTarget = GetTarget(source, target);
        StatusEffectManager.Instance.AddEffect(actualTarget, statusEffect);
    }
} 