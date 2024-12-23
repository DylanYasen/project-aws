using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public int maxEnergy = 3;
    public int currentEnergy;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        currentEnergy = maxEnergy;
        DeckManager.Instance.DrawCards(5); // Draw 5 cards at the start of the turn
        Debug.Log("Player turn started. Energy refreshed.");
    }

    public void EndPlayerTurn()
    {
        Debug.Log("Player turn ended.");
        // // Discard remaining hand
        // foreach (Card card in DeckManager.Instance.hand)
        // {
        //     DeckManager.Instance.discardPile.Add(card);
        // }
        // DeckManager.Instance.hand.Clear();

        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy turn started.");

        // @todo: Add enemy logic here
        yield return new WaitForSeconds(2);

        StartPlayerTurn();
    }
}