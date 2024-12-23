using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public int block;

    public int maxEnergy = 3;
    public int currentEnergy;

    CombatStatUI combatStatUI;

    SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        currentHP = maxHP;
        currentEnergy = maxEnergy;

        spriteRenderer = GetComponent<SpriteRenderer>();

        // @todo: improve
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position - Vector3.up);
            var rootCanvas = GameObject.Find("Canvas");
            var obj = Instantiate(GameManager.Instance.combatStatsUIPrefab, pos, Quaternion.identity, rootCanvas.transform);
            combatStatUI = obj.GetComponent<CombatStatUI>();
        }

        combatStatUI?.SetHealth(currentHP, maxHP);
        combatStatUI?.SetBlock(block);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;

        combatStatUI?.SetHealth(currentHP, maxHP);

        Debug.Log($"{name} took {damage} damage. Remaining HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }

        // LeanTween.moveLocal(gameObject, -transform.right * 0.2f, 0.25f).setEasePunch();
        LeanTween.color(gameObject, Color.red, 1f).setEasePunch();
    }

    public virtual void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);

        combatStatUI?.SetHealth(currentHP, maxHP);

        Debug.Log($"{name} healed for {amount} HP. Current HP: {currentHP}");
    }

    public virtual void Die()
    {
        Destroy(combatStatUI.gameObject);
        Destroy(gameObject);
    }

    public virtual void AddBlock(int block)
    {
        this.block += block;

        combatStatUI?.SetBlock(block);
    }

    protected static void ApplyCardEffect(Card card, Unit source, Unit target)
    {
        if (card.effects.Count > 0)
        {
            foreach (var effectEntry in card.effects)
            {
                effectEntry.cardEffect.Execute(source, target, effectEntry.effectValue);
            }
        }
    }

    public bool CanPlayCard(Card card)
    {
        return card.cost <= currentEnergy;
    }

    public virtual void StartTurn()
    {
        currentEnergy = maxEnergy;
    }
}
