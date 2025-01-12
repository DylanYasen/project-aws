using UnityEngine;

public class ApplyStatusEffect : CardEffect
{
    [SerializeReference]
    public StatusEffect statusEffect;

    public override void Execute(Unit source, Unit target)
    {
        if (target != null && statusEffect != null)
        {
            StatusEffectManager.Instance.AddEffect(target, statusEffect);
        }
    }
} 