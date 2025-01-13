using UnityEngine;

public class InvulnerableEffect : StatusEffect
{
    public InvulnerableEffect()
    {
        effectName = "Invulnerable";
        lifetimeType = StatusEffectLifetimeType.TurnBased;
        description = $"Prevents all incoming damage";
        isDebuff = false;
    }

    public override void OnApply(Unit target)
    {
        target.isInvulnerable = true;
    }

    public override void OnRemove(Unit target)
    {
        target.isInvulnerable = false;
    }

}