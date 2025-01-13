using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatStatUI : MonoBehaviour
{
    public TMP_Text blockAmountText;
    public TMP_Text healthText;

    public Image healthBar;

    public DebuffUI[] debuffs;


    private void Awake()
    {
        
    }

    public void SetDebuffs(List<StatusEffect> effects)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            debuffs[i].SetEffect(effects[i]);
            debuffs[i].ToggleVisibility(true);
        }

        for (int i = effects.Count; i < debuffs.Length; i++)
        {
            debuffs[i].ToggleVisibility(false);
        }
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
