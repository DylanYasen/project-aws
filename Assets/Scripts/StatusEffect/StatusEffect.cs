using UnityEngine;


[System.Serializable]
public abstract class StatusEffect
{
    public int duration = 1;
    public string effectName;
    public string description;
    public bool isDebuff;
    public Sprite icon;

    public StatusEffect()
    {
    }

    public virtual void OnApply(Unit target) { }
    public virtual void OnRemove(Unit target) { }
    public virtual void OnBeforeAttack(Unit source, Unit target, ref int damage) { }
    public virtual void OnTurnStart(Unit target) { }
    public virtual void OnTurnEnd(Unit target)
    {
        duration--;
    }
}