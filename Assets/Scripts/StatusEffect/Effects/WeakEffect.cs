using UnityEngine;

public class WeakEffect : StatusEffect
{
    public float damageReduction = 0.25f; // Reduces damage by 25%

    public WeakEffect()
    {
        effectName = "Weak";
        description = "Deals 25% less damage";
        isDebuff = true;
    }

   
    public override void OnPreApplyDamage(Unit attacker, Unit target, ref int damage)
    {
        int weakenedDamage = (int)((float)damage * (1.0f - damageReduction));
        Debug.Log($"Weakened damage for {attacker.name} from {damage} to {weakenedDamage}");
        damage = weakenedDamage;
    }
}
