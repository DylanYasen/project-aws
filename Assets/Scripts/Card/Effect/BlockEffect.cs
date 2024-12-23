using UnityEngine;

[CreateAssetMenu(fileName = "BlockEffect", menuName = "Card Effects/Block")]
public class BlockEffect : CardEffect
{
    public override void Execute(Unit source, Unit target, int effectValue)
    {
        CombatManager.Instance.ApplyBlock(target ? target : source, effectValue);
    }
}