using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public abstract void Execute(Unit source, Unit target, int effectValue);
}
