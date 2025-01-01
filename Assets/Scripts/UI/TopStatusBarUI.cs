using System;
using TMPro;
using UnityEngine;

public class TopStatusBarUI : MonoBehaviour
{
    public TMP_Text goldCountText;
    public TMP_Text healthText;

    void OnEnable()
    {
        GameManager.Instance.OnGoldChanged += UpdateGoldText;

        GameManager.Instance.OnPlayerHealthChanged += UpdateHealthText;
        CombatManager.Instance.OnUnitHealthChanged += UpdatePlayerHealthText;

        UpdateHealthText(GameManager.Instance.PlayerHealth, GameManager.Instance.PlayerMaxHealth);
    }

    private void UpdatePlayerHealthText(Unit unit, int currentHP, int maxHP)
    {
        if (unit != Player.Instance) return;
        healthText.text = $"{currentHP} / {maxHP}";
    }

    void OnDisable()
    {
        GameManager.Instance.OnGoldChanged -= UpdateGoldText;
    }

    public void UpdateHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void UpdateGoldText(int gold)
    {
        goldCountText.text = $"{gold}";
    }
}
