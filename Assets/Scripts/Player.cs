using UnityEngine;

public class Player : Unit
{
    public static Player Instance { get; private set; }

    EnergyBarUI energyBarUI;


    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();

        energyBarUI = GameObject.Find("UI EnergyBar").GetComponent<EnergyBarUI>();
        energyBarUI.SetEnergy(currentEnergy);
    }

    public void PlayCard(Card card, GameObject target = null)
    {
        if (currentHP < card.cost)
        {
            Debug.Log("Not enough energy!");
            return;
        }

        currentHP -= card.cost;
        energyBarUI.SetEnergy(currentHP);

        ApplyCardEffect(card, target);
        // discardPile.Add(card);
        Debug.Log($"Card played: {card.cardName} to target {target?.name}");
    }
}