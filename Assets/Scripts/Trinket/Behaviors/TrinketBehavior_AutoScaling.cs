using UnityEngine;

[Tooltip("Gain 2 block for every enemy on the field at the start of your turn.")]
public class TrinketBehavior_AutoScaling : TrinketBehavior
{
    public int blockPerEnemy = 2;

    private void OnTurnStart()
    {
        var enemyCount = EncounterManager.Instance.enemies.Count;
        Player.Instance.AddBlock(enemyCount * blockPerEnemy);
    }

    public override void OnAcquired()
    {
        TurnManager.Instance.OnPlayerTurnStart += OnTurnStart;
    }

    public override void OnRemoved()
    {
        TurnManager.Instance.OnPlayerTurnEnd -= OnTurnStart;
    }
}