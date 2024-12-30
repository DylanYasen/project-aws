using UnityEngine;

[Tooltip("Whenever you draw a card, gain 1 gold.")]
public class TrinketBehavior_DrawAmplifier : TrinketBehavior
{
    private void OnCardDrawn(Card card)
    {
        GameManager.Instance.AddGold(1);
    }

    public override void OnAcquired()
    {
        DeckManager.Instance.OnCardDrawn += OnCardDrawn;
    }

    public override void OnRemoved()
    {
        DeckManager.Instance.OnCardDrawn -= OnCardDrawn;
    }
} 