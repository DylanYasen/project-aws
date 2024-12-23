using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "Card Effects/Energy")]
public class Energy : CardEffect
{
    public override void Execute(Unit source, Unit target, int effectValue)
    {
        if (target != null)
        {
            target.currentEnergy += effectValue;
            if (target.currentEnergy > target.maxEnergy)
            {
                target.currentEnergy = target.maxEnergy;
            }
        }
    }
}