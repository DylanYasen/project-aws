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

    public void PlayCard(Card card, GameObject target = null)
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

        ApplyCardEffect(card, target);
        // discardPile.Add(card);
        Debug.Log($"Card played: {card.cardName} to target {target?.name}");


        if (currentEnergy == 0)
        {
            TurnManager.Instance.EndPlayerTurn();
        }
    }

}