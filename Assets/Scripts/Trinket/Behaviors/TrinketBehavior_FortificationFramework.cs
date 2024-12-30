using UnityEngine;

[Tooltip("Gain an additional 2 block whenever you gain block.")]
public class TrinketBehavior_FortificationFramework : TrinketBehavior
{
    private void OnBlockGained(ref int amount)
    {
        // Only add the bonus if we're actually gaining block
        if (amount > 0)
        {
            amount += 2;
        }
    }

    public override void OnAcquired()
    {
        // @todo: need something exposed to modify the block before it is applied
        // Player.Instance.OnBeforeBlockGained += OnBlockGained;
    }

    public override void OnRemoved()
    {
        // Player.Instance.OnBeforeBlockGained -= OnBlockGained;
    }
} 