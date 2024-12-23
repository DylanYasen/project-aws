using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "Card Effects/Energy")]
public class Energy : CardEffect
{
    public override void Execute(Unit source, Unit target, int effectValue)
    {
        var t = target ? target : source;
        t.AddEnergy(effectValue);
    }
}