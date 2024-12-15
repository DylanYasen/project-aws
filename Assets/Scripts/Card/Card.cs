using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardEffectEntry
{
    public CardEffect cardEffect;
    public int effectValue;
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public int cost; // Energy cost to play
    public Sprite cardArt; // Optional: Add visuals later
    public CardEffectType effectType; // Enum for Attack, Defense, Utility, etc.
    public int effectValue; // Damage, block amount, etc.

    public List<CardEffectEntry> effects = new List<CardEffectEntry>();
}

public enum CardEffectType
{
    Attack,
    Block,
    Heal,
    Utility
}
