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

    public override void OnApply(Unit unit)
    {
        CombatManager.Instance.ApplyDamage(null, unit, damagePerTurn);
        Debug.Log("DamageOverTimeEffect OnApply " + unit.name);
    }
}