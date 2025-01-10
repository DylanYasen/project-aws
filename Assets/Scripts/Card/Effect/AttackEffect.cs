using UnityEngine;

[System.Serializable]
public class AttackEffect : CardEffect
{
    public int damage;

    public override void Execute(Unit source, Unit target)
    {
        if (target != null)
        {
            CombatManager.Instance.ApplyDamage(source, target, damage);
        }
    }
}