using UnityEngine;

public class WeakEffect : StatusEffect
{
    public float damageReduction = 0.25f; // Reduces damage by 25%

    public WeakEffect()
    {
        effectName = "Weak";
        description = "Takes 25% less damage";
        isDebuff = true;
    }

    public override void OnApply(Unit target)
    {
        // Will implement damage reduction logic when we hook it up
        Debug.Log($"Applied Weak to {target.name}");
    }
}
