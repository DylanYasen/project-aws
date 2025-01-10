using UnityEngine;

public class BlockEffect : CardEffect
{
    public int blockAmount;
    public override void Execute(Unit source, Unit target )
    {
        CombatManager.Instance.ApplyBlock(target ? target : source, blockAmount);
    }
}