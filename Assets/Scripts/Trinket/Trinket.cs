using UnityEngine;

public enum TrinketRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(fileName = "New Trinket", menuName = "Game/Trinket")]
public class Trinket : ScriptableObject
{
    [Header("Display")]
    public string displayName;
    public string description;
    public Sprite icon;

    [Header("Properties")]
    public int price;
    public TrinketRarity rarity;
    public TrinketBehavior behavior;
}