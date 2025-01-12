using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager
{
    public static StatusEffectManager Instance { get; private set; }

    // Maps each unit to their list of active effects
    private Dictionary<Unit, List<StatusEffect>> effectsByUnit = new();

    public StatusEffectManager()
    {
        Instance = this;
    }

    public void AddEffect(Unit target, StatusEffect effect)
    {
        if (!effectsByUnit.ContainsKey(target))
        {
            effectsByUnit[target] = new List<StatusEffect>();
        }

        effectsByUnit[target].Add(effect);
        effect.OnApply(target);
    }

    public void RemoveEffect(Unit target, StatusEffect effect)
    {
        if (effectsByUnit.TryGetValue(target, out var effects))
        {
            if (effects.Remove(effect))
            {
                effect.OnRemove(target);
            }
        }
    }

    public List<StatusEffect> GetEffects(Unit target)
    {
        if (effectsByUnit.TryGetValue(target, out var effects))
        {
            return new List<StatusEffect>(effects); // Return a copy to prevent external modification
        }
        return new List<StatusEffect>();
    }

    public void OnTurnStart(Unit target)
    {
        if (!effectsByUnit.ContainsKey(target)) return;

        foreach (var effect in effectsByUnit[target])
        {
            effect.OnTurnStart(target);
        }
    }

    public void OnTurnEnd(Unit target)
    {
        if (!effectsByUnit.ContainsKey(target)) return;

        for (int i = effectsByUnit[target].Count - 1; i >= 0; i--)
        {
            var effect = effectsByUnit[target][i];
            effect.OnTurnEnd(target);
            if (effect.duration <= 0)
            {
                RemoveEffect(target, effect);
            }
        }
    }

    public void ClearAllEffects(Unit target)
    {
        if (effectsByUnit.TryGetValue(target, out var effects))
        {
            foreach (var effect in effects)
            {
                effect.OnRemove(target);
            }
            effects.Clear();
        }
    }
}
