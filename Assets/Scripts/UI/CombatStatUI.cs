using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatStatUI : MonoBehaviour
{
    public TMP_Text blockAmountText;
    public TMP_Text healthText;

    public Image healthBar;

    private void Awake()
    {
    }

    public void SetHealth(int currHealth, int maxHealth)
    {
        healthText.text = currHealth + "/" + maxHealth;
        healthBar.fillAmount = (float)currHealth / (float)maxHealth;
    }

    public void SetBlock(int block)
    {
        blockAmountText.text = block.ToString();
    }
}
