using UnityEngine;

public enum TargetType
{
    Target,  // Requires selecting a target
    Self     // Always targets the caster
}

[System.Serializable]
public abstract class CardEffect
{
    public TargetType targetType;

    protected Unit GetTarget(Unit source, Unit target)
    {
        return targetType == TargetType.Self ? source : target;
    }

    public abstract void Execute(Unit source, Unit target);
}
