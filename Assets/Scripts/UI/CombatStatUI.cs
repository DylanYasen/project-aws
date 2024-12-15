using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatStatUI : MonoBehaviour
{
    public TMP_Text blockAmountText;
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;

    public Image healthBar;

    private void Awake()
    {
    }

    public void SetHealth(int currHealth, int maxHealth)
    {
        // Update the health text
    }

    public void SetBlock(int block){

    }
}
