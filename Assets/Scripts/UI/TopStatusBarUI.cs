using System;
using TMPro;
using UnityEngine;

public class TopStatusBarUI : MonoBehaviour
{
    public TMP_Text goldCountText;
    public TMP_Text healthText;

    void Start()
    {
        GameManager.Instance.OnGoldChanged += UpdateGoldText;

        Player.Instance.OnHealthChanged += UpdateHealthText;

        UpdateGoldText(GameManager.Instance.Gold);
        UpdateHealthText(Player.Instance.currentHP, Player.Instance.maxHP);
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
