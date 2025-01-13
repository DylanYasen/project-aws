using UnityEngine;

public class VulnerableEffect : StatusEffect
{
    public float damageIncrease = 0.5f; // Takes 50% more damage

    public VulnerableEffect()
    {
        effectName = "Vulnerable";
        isDebuff = true;
        description = "Takes 50% more damage";
        // icon = Resources.Load<Sprite>("Icons/Vulnerable");
    }

    public override void OnApply(Unit target)
    {
        Debug.Log($"Applied Vulnerable to {target.name}");
    }

    public override void OnPreApplyDamage(Unit attacker, Unit target, ref int damage)
    {
        int newDamage = (int)((float)damage * (float)(1 + damageIncrease));
        Debug.Log($"Vulnerable damage increase: {damage} -> {newDamage} to {target.name}");
        damage = newDamage;
    }
}
