using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public virtual void Execute(Unit source, Unit target, int effectValue)
    {
    }
}
