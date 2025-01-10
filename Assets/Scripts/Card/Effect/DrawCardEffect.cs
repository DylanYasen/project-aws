using UnityEngine;

public class DrawCardEffect : CardEffect
{
    public int drawAmount;
    public override void Execute(Unit source, Unit target)
    {
        DeckManager.Instance.DrawCards(drawAmount);
    }
}