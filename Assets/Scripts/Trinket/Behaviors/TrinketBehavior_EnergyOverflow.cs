using UnityEngine;

[Tooltip("Gain 1 energy at the start of your turn if your hand is empty.")]
public class TrinketBehavior_EnergyOverflow : TrinketBehavior
{
    private void OnTurnStart()
    {
        if (DeckManager.Instance.hand.Count == 0)
        {
            Player.Instance.AddEnergy(1);
        }
    }

    public override void OnAcquired()
    {
        TurnManager.Instance.OnPlayerTurnStart += OnTurnStart;
    }

    public override void OnRemoved()
    {
        TurnManager.Instance.OnPlayerTurnStart -= OnTurnStart;
    }
} 