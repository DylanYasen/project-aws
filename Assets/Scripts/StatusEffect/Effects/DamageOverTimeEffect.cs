using UnityEngine;

public class DamageOverTimeEffect : StatusEffect
{
    public int damagePerTurn;

    public DamageOverTimeEffect()
    {
        effectName = "Damage Over Time";
        description = "Takes damage at the end of each turn";
        isDebuff = true;
        duration = 3;
        damagePerTurn = 5;
    }

    public override void OnTurnStart(Unit unit)
    {
        CombatManager.Instance.ApplyDamage(null, unit, damagePerTurn);
        duration--;
        if (duration <= 0)
        {
            StatusEffectManager.Instance.RemoveEffect(unit, this);
        }
    }
} 