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
    Specific    // Modify a specific card by name
}

public enum CardModificationTarget
{
    Hand,       // Cards in hand
    Deck        // Cards in draw pile
}

[System.Serializable]
public abstract class StatusEffect
{
    public StatusEffectLifetimeType lifetimeType;
    public int duration = 1;
    public string effectName;
    public string description;
    public bool isDebuff;
    public Sprite icon;

    public StatusEffect()
    {
    }

    public virtual void OnApply(Unit target)
    {
    }
    public virtual void OnRemove(Unit target) { }
    public virtual void OnBeforeAttack(Unit source, Unit target, ref int damage) { }
    public virtual void OnTurnStart(Unit target) { }
    public virtual void OnPreApplyDamage(Unit target, ref int damage) { }
    public virtual void OnTurnEnd(Unit target)
    {
        if (lifetimeType == StatusEffectLifetimeType.TurnBased)
        {
            duration--;
            if (duration <= 0)
            {
                StatusEffectManager.Instance.RemoveEffect(target, this);
            }
        }
    }
}