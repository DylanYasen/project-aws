using UnityEngine;


public class DamageReflectEffect : StatusEffect
{
    public float reflectPercentage = 0.5f;

    public DamageReflectEffect()
    {
        effectName = "Damage Reflect";
        isDebuff = false;
        lifetimeType = StatusEffectLifetimeType.TurnBased;
    }

    public override string GetDescription()
    {
        return $"Reflect {(reflectPercentage * 100)}% of incoming damage back to attacker";
    }

    public override void OnPreApplyDamage(Unit attacker, Unit target, ref int damage)
    {
        Debug.Log($"Damage Reflect Effect: {damage} {attacker.name} {target.name}");    
        if (damage > 0 && attacker != null && attacker != target)
        {
            int reflectedDamage = Mathf.RoundToInt(damage * reflectPercentage);
            // attacker.TakeDamage(reflectedDamage);
            Debug.Log($"{target.name} reflected {reflectedDamage} damage back to {attacker.name}");
            CombatManager.Instance.ApplyDamage(target, attacker, reflectedDamage);
        }
    }
}