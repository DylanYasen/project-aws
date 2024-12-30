using UnityEngine;

// Abstract base class for all trinket behaviors
public abstract class TrinketBehavior : ScriptableObject
{
    public abstract void OnAcquired();
    public abstract void OnRemoved();
} 