using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffectEntry
{
    [SerializeReference]
    public CardEffect cardEffect;
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

    public List<CardEffectEntry> effects = new();

    public Action<int> OnCostChanged;

    public void ModifyCost(int addition)
    {
        cost = Mathf.Max(0, cost + addition);
        OnCostChanged?.Invoke(cost);
    }
}
