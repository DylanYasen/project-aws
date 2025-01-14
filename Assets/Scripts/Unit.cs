using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHP = 100;
    public int maxEnergy = 3;

    public bool isStunned = false;
    public bool isInvulnerable = false;

    public int currentHP { get; protected set; }
    public int block { get; protected set; }
    public int currentEnergy { get; protected set; }

    public CombatStatUI combatStatUI;

    SpriteRenderer spriteRenderer;

    public event System.Action<int, int> OnHealthChanged;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Color originalColor;

    public enum UnitAnimationType
    {
        Attacking,
        Buffing,
        Healing,
        Blocking,
        TakingDamage,
        Death
    }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        originalScale = transform.localScale;
        originalColor = spriteRenderer.color;

        currentHP = maxHP;
        currentEnergy = maxEnergy;
    }

    public virtual void StartCombat()
    {
        // @todo: improve
        {
            var offset = (spriteRenderer.bounds.size.y / 2 + 0.1f);
            var pos = Camera.main.WorldToScreenPoint(transform.position - Vector3.up * offset);
            var rootCanvas = GameObject.Find("hp-bar-root");
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

        Animate(UnitAnimationType.TakingDamage);

        if (currentHP <= 0)
        {
            Die();
        }
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
        Animate(UnitAnimationType.Death);
        // Delay the actual destruction until animation completes
        LeanTween.delayedCall(1f, () => {
            Destroy(combatStatUI.gameObject);
            Destroy(gameObject);
        });
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

    public virtual void Animate(UnitAnimationType animType)
    {
        // Get the direction multiplier based on whether it's a player or not
        float directionMultiplier = (this is Player) ? 1f : -1f;

        switch (animType)
        {
            case UnitAnimationType.Attacking:
                LeanTween.cancel(gameObject);
                
                // Forward lunge - multiply by direction
                LeanTween.moveLocalX(gameObject, transform.localPosition.x + (0.2f * directionMultiplier), 0.2f)
                    .setEaseOutQuad()
                    .setOnComplete(() => {
                        LeanTween.moveLocalX(gameObject, originalPosition.x, 0.3f)
                            .setEaseOutBounce();
                    });
                break;

            case UnitAnimationType.Buffing:
                LeanTween.cancel(gameObject);
                
                // Scale up with pulse
                LeanTween.scale(gameObject, originalScale * 1.1f, 0.25f)
                    .setEaseInOutQuad()
                    .setLoopPingPong(1);
                
                // Glow effect
                LeanTween.color(gameObject, Color.yellow, 0.5f)
                    .setEaseInOutQuad()
                    .setLoopPingPong(1);
                break;

            case UnitAnimationType.Healing:
                LeanTween.cancel(gameObject);
                
                // Float up and down with rotation
                LeanTween.moveLocalY(gameObject, transform.localPosition.y + 0.1f, 0.5f)
                    .setEaseInOutQuad()
                    .setLoopPingPong(1);
                
                LeanTween.rotateZ(gameObject, 5f, 0.5f)
                    .setEaseInOutQuad()
                    .setLoopPingPong(1);
                
                // Green glow
                LeanTween.color(gameObject, Color.green, 0.5f)
                    .setEaseInOutQuad()
                    .setLoopPingPong(1);
                break;

            case UnitAnimationType.Blocking:
                LeanTween.cancel(gameObject);
                
                // Shift backward - multiply by direction
                LeanTween.moveLocalX(gameObject, transform.localPosition.x + (-0.1f * directionMultiplier), 0.2f)
                    .setEaseOutQuad()
                    .setLoopPingPong(1);
                
                // Scale vertically
                LeanTween.scaleY(gameObject, originalScale.y * 1.1f, 0.2f)
                    .setEaseOutQuad()
                    .setLoopPingPong(1);
                break;

            case UnitAnimationType.TakingDamage:
                LeanTween.cancel(gameObject);
                
                // Shake effect - multiply by direction
                LeanTween.moveLocalX(gameObject, transform.localPosition.x + (0.05f * directionMultiplier), 0.05f)
                    .setEaseShake()
                    .setLoopCount(6);
                
                // Red flash
                LeanTween.color(gameObject, Color.red, 0.3f)
                    .setEaseInOutQuad()
                    .setLoopPingPong(1);
                break;

            case UnitAnimationType.Death:
                LeanTween.cancel(gameObject);
                
                // Random rotation direction
                float rotationDirection = Random.value > 0.5f ? 1f : -1f;
                
                // Initial scale down and fade
                LeanTween.scale(gameObject, originalScale * 0.7f, 0.3f)
                    .setEaseOutQuad();
                
                LeanTween.alpha(gameObject, 0.5f, 0.3f)
                    .setEaseOutQuad()
                    .setOnComplete(() => {
                        // After initial scale/fade, do rotation and descent
                        LeanTween.rotateZ(gameObject, 15f * rotationDirection, 0.5f)
                            .setEaseInQuad();
                            
                        LeanTween.moveLocalY(gameObject, transform.localPosition.y - 0.25f, 0.5f)
                            .setEaseInQuad();
                            
                        // Final fade out and scale to zero
                        LeanTween.scale(gameObject, Vector3.zero, 0.5f)
                            .setEaseInQuad();
                            
                        LeanTween.alpha(gameObject, 0f, 0.5f)
                            .setEaseInQuad();
                    });
                break;
        }
    }
}
