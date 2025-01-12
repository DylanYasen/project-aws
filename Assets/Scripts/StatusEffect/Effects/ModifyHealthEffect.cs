using UnityEngine;

public class ModifyHealthEffect : StatusEffect
{
    public int hpAmount;
    public int maxHpAmount;

    public ModifyHealthEffect()
    {
        effectName = "Modify Health";
        // @todo: need a getDescription() for dynamic description
        // description = amount >= 0 ? $"Heal {amount} HP" : $"Take {-amount} damage";
        duration = 1; // Application based
    }

    public override void OnApply(Unit unit)
    {
        unit.SetHP(unit.currentHP + hpAmount);
        unit.SetMaxHP(unit.maxHP + maxHpAmount);
    }
}