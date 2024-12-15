using UnityEngine;

[CreateAssetMenu(fileName = "AttackEffect", menuName = "Card Effects/Attack")]
public class AttackEffect : CardEffect
{
    public override void Execute(Unit source, Unit target, int effectValue)
    {
        if (target != null)
        {
            CombatManager.Instance.ApplyDamage(source, target, effectValue);
        }
    }
}