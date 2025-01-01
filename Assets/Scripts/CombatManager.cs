using UnityEngine;
using System;

public class CombatManager
{
    public static CombatManager Instance;

    public event Action<Unit, int> OnBlockConsumed;

    public event Action<Unit, int> OnDamageTaken;

    public event Action<Unit, int, int> OnUnitHealthChanged;

    public CombatManager()
    {
        Instance = this;
    }

    public void ApplyDamage(Unit attacker, Unit target, int rawDamage)
    {
        if (target == null) return;

        int blockConsumed = Mathf.Min(target.block, rawDamage);
        if (blockConsumed > 0)
        {
            OnBlockConsumed?.Invoke(target, blockConsumed);
        }

        int damageAfterBlock = Mathf.Max(0, rawDamage - target.block);
        target.AddBlock(-rawDamage);

        target.TakeDamage(damageAfterBlock);

        // @todo: these events are kinda messy, clean up and consolidate
        OnDamageTaken?.Invoke(target, damageAfterBlock);
        OnUnitHealthChanged?.Invoke(target, target.currentHP, target.maxHP);

        Debug.Log($"{attacker.name} dealt {damageAfterBlock} damage to {target.name} (Raw: {rawDamage}, Block: {target.block}).");
    }

    public void ApplyHealing(Unit target, int healAmount)
    {
        if (target == null) return;

        target.Heal(healAmount);

        OnUnitHealthChanged?.Invoke(target, target.currentHP, target.maxHP);

        Debug.Log($"{target.name} healed for {healAmount} HP.");
    }

    public void ApplyBlock(Unit target, int blockAmount)
    {
        Debug.Log($"ApplyBlock called to {target.name}");
        if (target == null) return;

        target.AddBlock(blockAmount);
        Debug.Log($"{target.name} gained {blockAmount} block.");
    }
}
