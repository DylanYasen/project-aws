using UnityEngine;

[Tooltip("Start each combat with 1 extra card in hand.")]
public class TrinketBehavior_PreWarmedCache : TrinketBehavior
{
    private void OnCombatStart()
    {
        DeckManager.Instance.DrawCards(1);
    }

    public override void OnAcquired()
    {
        TurnManager.Instance.OnCombatStart += OnCombatStart;
    }

    public override void OnRemoved()
    {
        TurnManager.Instance.OnCombatStart -= OnCombatStart;
    }
} 