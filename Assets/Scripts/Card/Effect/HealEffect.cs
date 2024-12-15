using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Card Effects/Heal")]
public class HealEffect : CardEffect
{
    public override void Execute(Unit source, Unit target, int effectValue)
    {
        if (target != null)
        {
            CombatManager.Instance.ApplyHealing(target, effectValue);
        }
    }
}