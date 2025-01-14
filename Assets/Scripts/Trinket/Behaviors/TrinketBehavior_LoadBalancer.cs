using UnityEngine;

[Tooltip("Whenever you discard a card, gain 2 block.")]
public class TrinketBehavior_LoadBalancer : TrinketBehavior
{
    private void OnCardDiscarded(Card card)
    {
        CombatManager.Instance.ApplyBlock(Player.Instance, 2);
    }

    public override void OnAcquired()
    {
        //@todo: right now our play == discard. wire this up when end of round discard is implemented.

        // DeckManager.Instance.OnCardDiscarded += OnCardDiscarded;
    }

    public override void OnRemoved()
    {
        // DeckManager.Instance.OnCardDiscarded -= OnCardDiscarded;
    }
} 