using UnityEngine;

public class EnergyEffect : CardEffect
{
    public int energyAmount;
    
    public override void Execute(Unit source, Unit target)
    {
        var t = target ? target : source;
        t.AddEnergy(energyAmount);
    }
}