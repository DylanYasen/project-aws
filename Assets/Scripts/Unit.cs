using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public int block;

    public CombatStatUI combatStatUI;
 
    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (combatStatUI != null)
        {
            combatStatUI.SetHealth(currentHP, maxHP);
        }

        Debug.Log($"{name} took {damage} damage. Remaining HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);

        if (combatStatUI != null)
        {
            combatStatUI.SetHealth(currentHP, maxHP);
        }

        Debug.Log($"{name} healed for {amount} HP. Current HP: {currentHP}");
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void AddBlock(int block)
    {
        this.block += block;

        if(combatStatUI != null)
        {
            combatStatUI.SetBlock(block);
        }
    }
}
