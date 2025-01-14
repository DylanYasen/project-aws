using UnityEngine;

public class ModifyPlayerCardDrawCountEffect : StatusEffect
{
    public int drawCountModifier = -1;

    public ModifyPlayerCardDrawCountEffect()
    {
        effectName = "Draw Reduction";
        isDebuff = true;
        description = "Draw 1 less card each turn";
    }

    public override void OnApply(Unit target)
    {
        base.OnApply(target);
        Player.Instance.maxTurnStartDrawCount += drawCountModifier;
    }

    public override void OnRemove(Unit target)
    {
        base.OnRemove(target);
        Player.Instance.maxTurnStartDrawCount -= drawCountModifier;
    }
} 