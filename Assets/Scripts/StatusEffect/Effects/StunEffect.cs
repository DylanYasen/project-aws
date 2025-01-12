using UnityEngine;

public class StunEffect : StatusEffect
{
    public StunEffect()
    {
        effectName = "Stunned";
        description = "Cannot take actions this turn";
        isDebuff = true;
        duration = 1;
        // icon = Resources.Load<Sprite>("Icons/Stun");
    }

    public override void OnApply(Unit unit)
    {
        Debug.Log("StunEffect OnApply " + unit.name);
        unit.isStunned = true;
    }

    public override void OnTurnEnd(Unit unit)
    {
        duration--;
        if (duration <= 0)
        {
            unit.isStunned = false;
            StatusEffectManager.Instance.RemoveEffect(unit, this);
        }
    }
}