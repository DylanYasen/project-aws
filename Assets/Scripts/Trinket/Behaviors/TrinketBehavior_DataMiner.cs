using UnityEngine;

[Tooltip("Gain 2 gold for every card drawn.")]
public class TrinketBehavior_DataMiner : TrinketBehavior
{
    private void OnCardDrawn(Card card)
    {
        GameManager.Instance.AddGold(2);
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