using UnityEngine;

public class NextAttackCritEffect : StatusEffect
{
    public NextAttackCritEffect()
    {
        effectName = "Next Attack Crits";
        description = "Next attack deals double damage";
        isDebuff = false;
        duration = 1;
    }

    public override void OnBeforeAttack(Unit source, Unit target, ref int damage)
    {
        // Double the damage for critical hit
        damage *= 2;
        // Remove the effect after it's used
        StatusEffectManager.Instance.RemoveEffect(source, this);
    }
} 