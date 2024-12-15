using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CombatManager combatManager;

    public int energy = 3;

    public Card selectedCard;

    private void Awake()
    {
        Instance = this;
        combatManager = new CombatManager();
    }

    public void SelectCard(Card card)
    {
        selectedCard = card;
    }

    public void TargetEnemy(Enemy enemy)
    {
        if (selectedCard != null)
        {
            PlayCard(selectedCard);
            selectedCard = null;
        }
    }

    public void PlayCard(Card card, GameObject target = null)
    {
        if (energy < card.cost)
        {
            Debug.Log("Not enough energy!");
            return;
        }

        energy -= card.cost;
        ApplyCardEffect(card, target);
        // discardPile.Add(card);
        Debug.Log($"Card played: {card.cardName} to target {target?.name}");
    }

    private void ApplyCardEffect(Card card, GameObject targetObj)
    {
        if (card.effects.Count > 0)
        {
            var source = Player.Instance;
            Unit target = targetObj ? targetObj.GetComponent<Unit>() : null;
            foreach (var effect in card.effects)
            {
                if (effect.cardEffect is AttackEffect attackEffect)
                {
                    attackEffect.Execute(source, target, effect.effectValue);
                }
            }
        }
    }

    public void EndTurn()
    {
        energy = 3;
        Debug.Log("Turn ended. Energy reset to 3.");
    }

}