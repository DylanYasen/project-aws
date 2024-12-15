using UnityEngine;

[CreateAssetMenu(fileName = "AttackEffect", menuName = "Card Effects/Attack")]
public class AttackEffect : CardEffect
{
    public override void Execute()
    {
        // @todo: targeting
        Enemy target = null;

        if (target != null)
        {
            // target.TakeDamage(card.effectValue);
            // Debug.Log($"{card.cardName} dealt {card.effectValue} damage to {target.enemyName}.");
        }
    }
}