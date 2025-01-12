using UnityEngine;

public class ModifyHealthEffect : StatusEffect
{
    public int hpAmount;
    public int maxHpAmount;

    public ModifyHealthEffect()
    {
        effectName = "Modify Health";
        // @todo: need a getDescription() for dynamic description
        // description = amount >= 0 ? $"Heal {amount} HP" : $"Take {-amount} damage";
        duration = 1; // Application based
    }

    public override void OnApply(Unit unit)
    {
        if (unit == Player.Instance)
        {
            if (Player.Instance)
            {
                Player.Instance.SetHP(Player.Instance.currentHP + hpAmount);
                Player.Instance.SetMaxHP(Player.Instance.maxHP + maxHpAmount);
            }
            else
            {
                GameManager.Instance.ModifyPlayerHealth(hpAmount);
                GameManager.Instance.ModifyPlayerMaxHealth(maxHpAmount);
            }
        }
        else
        {
            unit.SetHP(unit.currentHP + hpAmount);
            unit.SetMaxHP(unit.maxHP + maxHpAmount);
        }
    }
}