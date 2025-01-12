using UnityEngine;

public class ThrottledEffect : StatusEffect
{
    public float actionSpeedReduction = 0.5f; // Reduces action speed by 50%

    public ThrottledEffect()
    {
        effectName = "Throttled";
        isDebuff = true;
    }

    public override void OnApply(Unit target)
    {
        Debug.Log($"Applied Throttled to {target.name}");
    }
}
