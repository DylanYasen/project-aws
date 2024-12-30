using UnityEngine;

[Tooltip("Retain up to 5 block between turns.")]
public class TrinketBehavior_ElasticBuffers : TrinketBehavior
{
    private void OnTurnEnd()
    {
        // int currentBlock = Player.Instance.Block;
        // if (currentBlock > 0)
        // {
        //     // Save up to 5 block
        //     int blockToRetain = Mathf.Min(currentBlock, 5);
        //     Player.Instance.SetBlock(blockToRetain);
        // }
    }

    public override void OnAcquired()
    {
        // @todo: figure out the best time to retain block
        // TurnManager.Instance.OnPlayerTurnEnd += OnTurnEnd;
    }

    public override void OnRemoved()
    {
        // TurnManager.Instance.OnPlayerTurnEnd -= OnTurnEnd;
    }
} 