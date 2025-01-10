using UnityEngine;

public class DamageAllEffect : CardEffect
{
    public int damageAmount;

    public override void Execute(Unit source, Unit target)
    {
        foreach (var enemy in EncounterManager.Instance.enemies)
        {
            CombatManager.Instance.ApplyDamage(source, enemy, damageAmount);
        }
    }
}