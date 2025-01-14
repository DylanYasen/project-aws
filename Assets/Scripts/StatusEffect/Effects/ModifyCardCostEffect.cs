using UnityEngine;
using System.Collections.Generic;

public class ModifyCardCostEffect : StatusEffect
{
    struct ModificationRecord
    {
        public Card card;
        public int originalCost;
    }

    public CardModificationType modificationType;
    public CardModificationTarget target;
    public string targetCardName;
    public int costChange; // Positive = increase cost, Negative = decrease cost, < -9 = remove cost

    List<ModificationRecord> modificationRecords = new List<ModificationRecord>();

    public ModifyCardCostEffect()
    {
        effectName = "Modify Card Cost";
        isDebuff = costChange > 0;
        duration = 1;
    }

    public override void OnApply(Unit unit)
    {
        if (unit != Player.Instance) return;

        var deck = DeckManager.Instance;

        if (modificationType == CardModificationType.All)
        {
            foreach (var targetCard in deck.hand)
            {
                modificationRecords.Add(new ModificationRecord { card = targetCard, originalCost = targetCard.cost });
                targetCard.ModifyCost(costChange);
                Debug.Log($"Modified {targetCard.name}'s cost by {costChange}. New cost: {targetCard.cost}");

                deck.RefreshCardUI(targetCard);
            }
        }
        else
        {

            Card targetCard = null;
            // Find target card based on location
            if (modificationType == CardModificationType.Random)
            {
                targetCard = target switch
                {
                    CardModificationTarget.Deck => deck.GetRandomCardInDeck(),
                    CardModificationTarget.Hand => deck.GetRandomCardInHand(),
                    _ => null
                };
            }
            else if (modificationType == CardModificationType.Specific)
            {
                throw new System.NotImplementedException("Specific card targeting not implemented");
            }
            else if (modificationType == CardModificationType.LastDrawn)
            {
                targetCard = deck.lastDrawnCard;
            }

            if (targetCard == null)
            {
                Debug.LogWarning($"No card found to modify in {target}");
                return;
            }

            modificationRecords.Add(new ModificationRecord { card = targetCard, originalCost = targetCard.cost });


            // Modify the card's cost
            targetCard.ModifyCost(costChange);
            Debug.Log($"Modified {targetCard.name}'s cost by {costChange}. New cost: {targetCard.cost}");

            deck.RefreshCardUI(targetCard);

        }

    }

    public override void OnRemove(Unit unit)
    {
        if (unit != Player.Instance) return;

        foreach (var record in modificationRecords)
        {
            record.card.cost = record.originalCost;
            DeckManager.Instance.RefreshCardUI(record.card);
        }
    }
}