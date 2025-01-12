using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHP = 100;
    public int maxEnergy = 3;

    public bool isStunned = false;

    public int currentHP { get; protected set; }
    public int block { get; protected set; }
    public int currentEnergy { get; protected set; }

    protected CombatStatUI combatStatUI;

    SpriteRenderer spriteRenderer;

    public event System.Action<int, int> OnHealthChanged;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHP = maxHP;
        currentEnergy = maxEnergy;
    }

    public virtual void StartCombat()
    {
        // @todo: improve
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position - Vector3.up);
            var rootCanvas = GameObject.Find("Canvas");
            var obj = Instantiate(GameManager.Instance.combatStatsUIPrefab, pos, Quaternion.identity, rootCanvas.transform);
            combatStatUI = obj.GetComponent<CombatStatUI>();
        }

        combatStatUI?.SetHealth(currentHP, maxHP);
        combatStatUI?.SetBlock(block);

        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public virtual void SetVisible(bool visible)
    {
        spriteRenderer.enabled = visible;
        combatStatUI?.gameObject.SetActive(visible);
    }

    public virtual void SetHP(int amount)
    {
        currentHP = Mathf.Clamp(amount, 0, maxHP);
        combatStatUI?.SetHealth(currentHP, maxHP);

        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public virtual void SetMaxHP(int amount)
    {
        maxHP = amount;
        currentHP = Mathf.Min(currentHP, maxHP);
        combatStatUI?.SetHealth(currentHP, maxHP);

        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;

        combatStatUI?.SetHealth(currentHP, maxHP);
        combatStatUI?.SetBlock(block);

        OnHealthChanged?.Invoke(currentHP, maxHP);

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

        OnHealthChanged?.Invoke(currentHP, maxHP);

        Debug.Log($"{name} healed for {amount} HP. Current HP: {currentHP}");
    }

    public virtual void AddEnergy(int amount)
    {
        // currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        currentEnergy += amount;
    }

    public virtual void Die()
    {
        Destroy(combatStatUI.gameObject);
        Destroy(gameObject);
    }

    public virtual void AddBlock(int amount)
    {
        block = Mathf.Clamp(block + amount, 0, int.MaxValue); //@todo: max block?

        combatStatUI?.SetBlock(block);
    }

    protected static void ApplyCardEffect(Card card, Unit source, Unit target)
    {
        if (card.effects.Count > 0)
        {
            foreach (var effectEntry in card.effects)
            {
                effectEntry.cardEffect.Execute(source, target);
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

        // @todo: block retention
        block = 0;
    }
}
