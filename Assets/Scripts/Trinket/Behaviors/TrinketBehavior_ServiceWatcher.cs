using UnityEngine;

[Tooltip("Gain 1 block whenever an enemy plays a debuff.")]
public class TrinketBehavior_ServiceWatcher : TrinketBehavior
{
    // @todo: wire this up when we have status effect
    // private void OnDebuffApplied(StatusEffect debuff)
    // {

    // }

    public override void OnAcquired()
    {
        // Player.Instance.OnStatusEffectApplied += OnDebuffApplied;
    }

    public override void OnRemoved()
    {
        // Player.Instance.OnStatusEffectApplied -= OnDebuffApplied;
    }
}