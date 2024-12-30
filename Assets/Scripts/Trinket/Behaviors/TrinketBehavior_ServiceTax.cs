using UnityEngine;

[Tooltip("Gain 1 gold for every enemy attack targeting you.")]
public class TrinketBehavior_ServiceTax : TrinketBehavior
{
    private void OnBeingAttacked(int damage)
    {
        GameManager.Instance.AddGold(1);
    }

    public override void OnAcquired()
    {
        // @todo: figure out a way to wire this up

        // Player.Instance.OnBeingAttacked += OnBeingAttacked;
    }

    public override void OnRemoved()
    {
        // Player.Instance.OnBeingAttacked -= OnBeingAttacked;
    }
}