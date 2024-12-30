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
    public Sprite icon;

    [Header("Properties")]
    public int price;
    public TrinketRarity rarity;
    // Reference to the behavior implementation
    public TrinketBehavior behavior;
}