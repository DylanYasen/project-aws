using UnityEngine;

public class ModifyInitialEnergyEffect : StatusEffect
{
    public int energyModifier = -1;

    public ModifyInitialEnergyEffect()
    {
        effectName = "Energy Loss";
        isDebuff = true;
        description = "Start each turn with 1 less Energy";
    }

    public override void OnApply(Unit target)
    {
        base.OnApply(target);

        target.maxEnergy += energyModifier;
    }

    public override void OnRemove(Unit target)
    {
        base.OnRemove(target);
        target.maxEnergy -= energyModifier;
    }
}