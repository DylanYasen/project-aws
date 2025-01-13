using UnityEngine;

public class RemoveCardInHandEffect : CardEffect
{
    public CardModificationType modificationType;

    // [ShowIf("modificationType", ModificationType.Specific)]
    public int amount = 1;
    public string targetCardName;

    override public void Execute(Unit source, Unit target)
    {
        var deckManager = DeckManager.Instance;

        for (int i = 0; i < amount; i++)
        {
            Card targetCard = null;

            if (modificationType == CardModificationType.Specific)
            {
                targetCard = deckManager.FindCardInHandByName(targetCardName);
            }
            else if (modificationType == CardModificationType.Random)
            {
                targetCard = deckManager.GetRandomCardInHand();
            }
            if (targetCard != null)
            {
                deckManager.RemoveCardFromHand(targetCard);
            }
        }
    }
}