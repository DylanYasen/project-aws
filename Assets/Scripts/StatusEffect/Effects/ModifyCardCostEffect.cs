using UnityEngine;

public class ModifyCardCostEffect : StatusEffect
{
    public CardModificationType modificationType;
    public CardModificationTarget target;
    public string targetCardName;
    public int costChange; // Positive = increase cost, Negative = decrease cost

    public ModifyCardCostEffect()
    {
        effectName = "Modify Card Cost";
        isDebuff = costChange > 0;
        duration = 1;
    }

    public string GetDescription()
    {
        string changeText = costChange > 0 ? $"Increase cost by {costChange}" : $"Decrease cost by {-costChange}";
        string targetText = modificationType == CardModificationType.Random
            ? "a random card"
            : $"'{targetCardName}'";

        string locationText = target switch
        {
            CardModificationTarget.Hand => "in hand",
            CardModificationTarget.Deck => "in deck",
            _ => ""
        };

        return $"{changeText} of {targetText} {locationText}";
    }

    public override void OnApply(Unit unit)
    {
        if (unit != Player.Instance) return;

        var deck = DeckManager.Instance;
        Card targetCard = null;

        // Find target card based on location
        if (modificationType == CardModificationType.Random)
        {
            targetCard = target switch
            {
                CardModificationTarget.Deck => deck.GetRandomCardInDeck(),
                // CardModificationTarget.Hand => deck.GetRandomCardInHand(),
                _ => null
            };
        }
        else if (modificationType == CardModificationType.Specific)
        {            
            throw new System.NotImplementedException("Specific card targeting not implemented");
        }

        if (targetCard == null)
        {
            Debug.LogWarning($"No card found to modify in {target}");
            return;
        }

        // Modify the card's cost
        targetCard.cost = Mathf.Max(0, targetCard.cost + costChange);
        Debug.Log($"Modified {targetCard.name}'s cost by {costChange}. New cost: {targetCard.cost}");
    }
} 