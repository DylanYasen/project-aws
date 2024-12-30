using UnityEngine;

[CreateAssetMenu(fileName = "AdaptiveShield", menuName = "Game/Trinket Behaviors/Adaptive Shield")]
[Tooltip("Gain block for each card drawn on your turn.")]
public class TrinketBehavior_AdaptiveShield : TrinketBehavior
{
    private void OnCardDrawn(Card card)
    {
        Player.Instance.AddBlock(1);
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
