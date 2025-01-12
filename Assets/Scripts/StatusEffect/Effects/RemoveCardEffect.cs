using UnityEngine;

public class RemoveCardEffect : StatusEffect
{
    public CardModificationType modificationType;
    public CardModificationTarget target;

    // [ShowIf("modificationType", ModificationType.Specific)]
    public string targetCardName;

    public RemoveCardEffect()
    {
        effectName = "Remove Card";
        isDebuff = true;
        duration = 1;
    }

    public string GetDescription()
    {
        string targetText = modificationType == CardModificationType.Random
            ? "a random card"
            : $"'{targetCardName}'";

        string locationText = target switch
        {
            CardModificationTarget.Hand => "from hand",
            CardModificationTarget.Deck => "from deck",
            _ => ""
        };

        return $"Remove {targetText} {locationText}";
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
            throw new System.NotImplementedException("Specific card removal not implemented");
        }

        if (targetCard == null)
        {
            Debug.LogWarning($"No card found to remove in {target}");
            return;
        }

        if (target == CardModificationTarget.Deck)
        {
            deck.RemoveCardFromDeck(targetCard);
        }
        else if (target == CardModificationTarget.Hand)
        {
            throw new System.NotImplementedException("Hand card removal not implemented");
        }
    }
}