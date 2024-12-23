using UnityEngine;

public class Player : Unit
{
    public int maxHandSize = 5;
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

    public override void StartTurn()
    {
        base.StartTurn();

        currentEnergy = maxEnergy;
        energyBarUI.SetEnergy(currentEnergy);

        DeckManager.Instance.DrawCards(maxHandSize - DeckManager.Instance.hand.Count);
    }

    public void PlayCard(Card card, GameObject targetObj = null)
    {
        if (currentEnergy < card.cost)
        {
            Debug.Log("Not enough energy!");
            return;
        }

        {
            currentEnergy -= card.cost;
            energyBarUI.SetEnergy(currentEnergy);
        }

        var target = targetObj ? targetObj.GetComponent<Unit>() : null;

        ApplyCardEffect(card, this, target);
        Debug.Log($"Card played: {card.cardName} to target {target?.name}");

        DeckManager.Instance.DiscardCard(card);

        if (currentEnergy == 0)
        {
            TurnManager.Instance.EndPlayerTurn();
        }
    }

    public override void AddEnergy(int amount)
    {
        base.AddEnergy(amount);
        energyBarUI.SetEnergy(currentEnergy);
    }

}