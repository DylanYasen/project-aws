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

    public List<CardEffectEntry> effects = new();
}
