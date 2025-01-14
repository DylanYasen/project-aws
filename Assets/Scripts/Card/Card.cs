using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffectEntry
{
    [SerializeReference]
    public CardEffect cardEffect;
}

public enum CardConditon
{
    None,
    CardPlayedThisTurn
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public int cost; // Energy cost to play 
    public Sprite cardArt; // Optional: Add visuals later
    public bool requiresTarget;
    public int price;

    public int cooldown; // used for enemy cards

    public CardConditon condition;

    public Unit.UnitAnimationType unitAnimType = Unit.UnitAnimationType.Attacking;

    public List<CardEffectEntry> effects = new();

    public Action<int> OnCostChanged;

    public void ModifyCost(int addition)
    {
        cost = Mathf.Max(0, cost + addition);
        OnCostChanged?.Invoke(cost);
    }

    public bool IsConditionMet()
    {
        switch (condition)
        {
            case CardConditon.None:
                return true;
            case CardConditon.CardPlayedThisTurn:
                return Player.Instance.CardsPlayedThisTurn > 0;
            default:
                return true;
        }
    }
}
