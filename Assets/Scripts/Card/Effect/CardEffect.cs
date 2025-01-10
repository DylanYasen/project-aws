using UnityEngine;

[System.Serializable]
public abstract class CardEffect
{
    public virtual void Execute(Unit source, Unit target)
    {
    }
}
