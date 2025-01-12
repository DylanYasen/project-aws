using UnityEngine;

public class ModifyGoldEffect : StatusEffect
{
    public int goldAmount;

    public ModifyGoldEffect()
    {
        effectName = "Modify Gold";
        description = goldAmount >= 0 ? $"Gain {goldAmount} gold" : $"Lose {-goldAmount} gold";
        isDebuff = goldAmount < 0;
        duration = 1; // Application based
    }

    public override void OnApply(Unit unit)
    {
        if (unit == Player.Instance)
        {
            GameManager.Instance.AddGold(goldAmount);
        }
    }
} 