using UnityEngine;

public class DelayedBlockEffect : StatusEffect
{
    public int blockAmount;

    public DelayedBlockEffect()
    {
        effectName = "Delayed Block";
        isDebuff = false;
        lifetimeType = StatusEffectLifetimeType.TurnBased;
        // icon = Resources.Load<Sprite>("Icons/DelayedBlock");
    }

    public override string GetDescription()
    {
        return $"Gain {blockAmount} Block when this effect expires";
    }

    public override void OnRemove(Unit unit)
    {
        unit.AddBlock(blockAmount);
        Debug.Log($"DelayedBlock expired: {unit.name} gained {blockAmount} Block");
    }
} 