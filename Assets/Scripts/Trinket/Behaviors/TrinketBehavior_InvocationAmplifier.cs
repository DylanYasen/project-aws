using UnityEngine;

[Tooltip("When you use an ability that targets all enemies, deal 2 additional damage to each.")]
public class TrinketBehavior_InvocationAmplifier : TrinketBehavior
{
    private void OnCardPlayed(Card card)
    {
        // @todo: need to tag multi target cards
        
        // if (card.targetType == TargetType.AllEnemies)
        // {
        //     foreach (var enemy in CombatManager.Instance.enemies)
        //     {
        //         enemy.TakeDamage(2);
        //     }
        // }
    }

    public override void OnAcquired()
    {
        DeckManager.Instance.OnCardPlayed += OnCardPlayed;
    }

    public override void OnRemoved()
    {
        DeckManager.Instance.OnCardPlayed -= OnCardPlayed;
    }
} 