using UnityEngine;
using System;

public class CombatManager
{
    public static CombatManager Instance;

    public event Action<Unit, int> OnBlockConsumed;
    public event Action<Unit, int> OnDamageTaken;

    public CombatManager()
    {
        Instance = this;
    }

    public void ApplyDamage(Unit attacker, Unit target, int rawDamage)
    {
        if (target == null) return;

        Debug.Log($"Applying damage: {rawDamage} to {target.name}");

        if (target.isInvulnerable)
        {
            Debug.Log($"{target.name} is invulnerable, damage not applied");
            return;
        }

        StatusEffectManager.Instance.OnPreApplyDamage(attacker, target, ref rawDamage);

        Debug.Log($"Post Effect Applying damage: {rawDamage} to {target.name}");

        int blockConsumed = Mathf.Min(target.block, rawDamage);
        if (blockConsumed > 0)
        {
            OnBlockConsumed?.Invoke(target, blockConsumed);
        }

        int damageAfterBlock = Mathf.Max(0, rawDamage - target.block);
        target.AddBlock(-rawDamage);

        target.TakeDamage(damageAfterBlock);

        // Track player damage
        if (attacker == Player.Instance)
        {
            Player.Instance.AddDamageDealt(damageAfterBlock);
        }

        // @todo: these events are kinda messy, clean up and consolidate
        OnDamageTaken?.Invoke(target, damageAfterBlock);

        Debug.Log($"{attacker?.name} dealt {damageAfterBlock} damage to {target.name} (Raw: {rawDamage}, Block: {target.block}).");
    }

    public void ApplyHealing(Unit target, int healAmount)
    {
        if (target == null) return;

        target.Heal(healAmount);

        Debug.Log($"{target.name} healed for {healAmount} HP.");
    }

    public void ApplyBlock(Unit target, int blockAmount)
    {
        Debug.Log($"Applying block to {target.name}");

        if (target == null) return;

        StatusEffectManager.Instance.OnPreGainBlock(target, ref blockAmount);

        Debug.Log($"Post Effect Applying block: {blockAmount} to {target.name}");

        target.AddBlock(blockAmount);

        Debug.Log($"{target.name} gained {blockAmount} block.");
    }
}
