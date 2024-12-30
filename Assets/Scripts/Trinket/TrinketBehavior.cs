using UnityEngine;

// Abstract base class for all trinket behaviors
public abstract class TrinketBehavior : ScriptableObject
{
    public abstract void OnAcquired(Player player);
    public abstract void OnRemoved(Player player);
} 