using UnityEngine;

public enum StatusEffectLifetimeType
{
    TurnBased,
    EncounterBased,
    OneOff,
    Permanent
}

public enum CardModificationType
{
    Random,     // Modify a random card
    Specific,    // Modify a specific card by name,
    LastDrawn,    // Modify the last drawn card,
    All,         // Modify all cards
}

public enum CardModificationTarget
{
    Hand,       // Cards in hand
    Deck        // Cards in draw pile
}

public enum StatusEffectApplicationType
{
    OnApply,
    OnRemove,
    OnTurnStart,
    OnTurnEnd,
}

[System.Serializable]
public abstract class StatusEffect
{
    public StatusEffectLifetimeType lifetimeType;
    public int duration = 1;
    public string effectName;
    public string description;
    public bool isDebuff;
    public bool isHidden;
    public Sprite icon;
    public StatusEffectApplicationType applicationType;

    public StatusEffect()
    {
    }

    public virtual void OnApply(Unit target)
    {
    }
    public virtual void OnRemove(Unit target) { }
    public virtual void OnBeforeAttack(Unit source, Unit target, ref int damage) { }
    public void OnTurnStart(Unit target)
    {
    }
    public virtual void OnPreApplyDamage(Unit attacker, Unit target, ref int damage) { }
    public void OnTurnEnd(Unit target)
    {
    }

    public virtual void OnPreGainBlock(Unit target, ref int blockAmount) { }

    public virtual string GetDescription()
    {
        return description;
    }
}