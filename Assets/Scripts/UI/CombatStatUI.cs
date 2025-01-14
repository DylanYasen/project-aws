using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatStatUI : MonoBehaviour
{
    public TMP_Text blockAmountText;
    public TMP_Text healthText;
    public TMP_Text displayNameText;

    public Image healthBar;

    public DebuffUI[] debuffs;


    private void Awake()
    {
        
    }

    public void SetDebuffs(List<StatusEffect> effects)
    {
        int index = 0;
        foreach (var effect in effects)
        {
            if (effect.isHidden) continue;
            debuffs[index].SetEffect(effect);
            debuffs[index].ToggleVisibility(true);
            index++;
        }

        for (int i = index; i < debuffs.Length; i++)
        {
            debuffs[i].SetEffect(null);
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
