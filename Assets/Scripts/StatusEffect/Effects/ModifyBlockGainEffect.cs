using UnityEngine;

public class ModifyBlockGainEffect : StatusEffect
{
    public bool flatIncrease = false;
    public float blockModifier = -0.25f;
    public int flatIncreaseAmount = 0;

    public ModifyBlockGainEffect()
    {
        effectName = "Frail";  // Default name, can be changed when instantiated
        isDebuff = true;       // Default as debuff since it reduces block
        description = "Reduces block gain by 25%";
    }

    public override void OnPreGainBlock(Unit target, ref int blockAmount)
    {
        int newBlock = 0;
        if (flatIncrease)
        {
            newBlock = blockAmount + flatIncreaseAmount;
        }
        else
        {
            newBlock = (int)((float)blockAmount * (1 + blockModifier));
        }
        Debug.Log($"{effectName} modified block gain: {blockAmount} -> {newBlock} for {target.name}");
        blockAmount = newBlock;
    }
}