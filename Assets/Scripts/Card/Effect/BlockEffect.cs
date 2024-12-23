using UnityEngine;

[CreateAssetMenu(fileName = "BlockEffect", menuName = "Card Effects/Block")]
public class BlockEffect : CardEffect
{
    public override void Execute(Unit source, Unit target, int effectValue)
    {
        if (target != null)
        {
            CombatManager.Instance.ApplyBlock(target, effectValue);
        }
    }
}