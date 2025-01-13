using UnityEngine;

[System.Serializable]
public class AttackEffect : CardEffect
{
    public int damage;

    public AttackEffect()
    {
        targetType = TargetType.Target; // Attacks always need a target
    }

    public override void Execute(Unit source, Unit target)
    {
        Unit actualTarget = GetTarget(source, target);
        CombatManager.Instance.ApplyDamage(source, actualTarget, damage);
    }
}