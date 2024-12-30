using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GoldPerCardPlayedBehavior", menuName = "Trinkets/Behaviors/Gold Per Card Played")]
[Tooltip("Gain gold equal to the cost of the card played.")]
public class GoldPerCardPlayedBehavior : TrinketBehavior
{
    private void OnCardPlayed(Card card)
    {
        GameManager.Instance.AddGold(card.cost);
    }

    public override void OnAcquired()
    {
        DeckManager.Instance.OnCardPlayed += OnCardPlayed;
    }

    public override void OnRemoved()
    {
        DeckManager.Instance.OnCardPlayed -= OnCardPlayed;
    }
}