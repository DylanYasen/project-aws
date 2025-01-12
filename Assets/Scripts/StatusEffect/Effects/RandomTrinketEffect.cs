using UnityEngine;

public class RandomTrinketEffect : StatusEffect
{
    public RandomTrinketEffect()
    {
        effectName = "Random Trinket";
        description = "Receive a random trinket";
        isDebuff = false;
        duration = 1; // Application based
    }

    public override void OnApply(Unit unit)
    {
        if (unit == Player.Instance)
        {
            var trinket = TrinketManager.Instance.GetRandomTrinket();
            if (trinket != null)
            {
                TrinketManager.Instance.AddTrinketToPlayer(trinket);
                Debug.Log($"Awarded random trinket: {trinket.name}");
            }
            else
            {
                Debug.LogWarning("No trinkets available to award");
            }
        }
    }
} 