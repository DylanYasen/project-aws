using UnityEngine;

[Tooltip("Gain 5 gold every time you use all available energy in a turn.")]
public class TrinketBehavior_LambdaBooster : TrinketBehavior
{
    private void OnTurnEnd()
    {
        if (Player.Instance.currentEnergy == 0)
        {
            GameManager.Instance.AddGold(5);
        }
    }

    public override void OnAcquired()
    {
        TurnManager.Instance.OnPlayerTurnEnd += OnTurnEnd;
    }

    public override void OnRemoved()
    {
        TurnManager.Instance.OnPlayerTurnEnd -= OnTurnEnd;
    }
} 