using UnityEngine;

[Tooltip("Doubles recovery effects during maintenance.")]
public class TrinketBehavior_EngineeringExcellence : TrinketBehavior
{
    private void OnHealingReceived(ref int amount)
    {
        // Only double healing during rest sites
        // if (GameManager.Instance.currentScene == "RestSiteScene")
        // {
        //     amount *= 2;
        // }
    }

    public override void OnAcquired()
    {
        // @todo: figure out the best time to double healing and block
        // Player.Instance.OnBeforeHealing += OnHealingReceived;
    }

    public override void OnRemoved()
    {
        // Player.Instance.OnBeforeHealing -= OnHealingReceived;
    }
} 