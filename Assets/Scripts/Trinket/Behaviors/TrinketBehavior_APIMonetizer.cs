using UnityEngine;

[Tooltip("Gain 1 gold every time you play a 0-cost card.")]
public class TrinketBehavior_APIMonetizer : TrinketBehavior
{
    private void OnCardPlayed(Card card)
    {
        if (card.cost == 0)
        {
            GameManager.Instance.AddGold(1);
        }
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