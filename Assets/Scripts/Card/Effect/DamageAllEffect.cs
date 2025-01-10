using UnityEngine;

public class DamageAllEffect : CardEffect
{
    public int damageAmount;

    public override void Execute(Unit source, Unit target)
    {
        // doing it reversed, so we can remove enemies as they die
        var enemies = EncounterManager.Instance.enemies;
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            CombatManager.Instance.ApplyDamage(source, enemies[i], damageAmount);
        }
    }
}