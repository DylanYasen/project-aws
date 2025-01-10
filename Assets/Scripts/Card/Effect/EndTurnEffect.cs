using UnityEngine;

public class EndTurnEffect : CardEffect
{
    public override void Execute(Unit source, Unit target)
    {
        TurnManager.Instance.EndPlayerTurn();
    }
} 