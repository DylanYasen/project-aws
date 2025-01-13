using UnityEngine;

public enum EffectModificationType
{
    Random,
    All
}

public class RemoveStatusEffectEffect : CardEffect
{
    public bool removeDebuffsOnly = true;
    public EffectModificationType modificationType;

    public override void Execute(Unit source, Unit target)
    {
        var effects = StatusEffectManager.Instance.GetEffectsForUnit(target);

        if (effects.Count == 0) return;

        var validEffects = effects.FindAll(effect => !removeDebuffsOnly || effect.isDebuff);

        if (modificationType == EffectModificationType.Random)
        {
            int randomIndex = Random.Range(0, validEffects.Count);
            var effectToRemove = validEffects[randomIndex];
            Debug.Log($"Removing {effectToRemove.effectName} from {target.name}");
            StatusEffectManager.Instance.RemoveEffect(target, effectToRemove);
        }
        else if (modificationType == EffectModificationType.All)
        {
            foreach (var effect in validEffects)
            {
                StatusEffectManager.Instance.RemoveEffect(target, effect);
                Debug.Log($"Removing {effect.effectName} from {target.name}");
            }
        }
    }
}