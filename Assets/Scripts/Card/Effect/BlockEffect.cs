using UnityEngine;

public class BlockEffect : CardEffect
{
    public int blockAmount;
    public override void Execute(Unit source, Unit target)
    {
        Unit actualTarget = GetTarget(source, target);
        CombatManager.Instance.ApplyBlock(actualTarget, blockAmount);
    }
}