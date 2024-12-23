using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    EnemyTurn,
    EndOfRound
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    private TurnState currentState;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void Init()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        currentState = TurnState.PlayerTurn;

        Player.Instance.StartTurn();
        Debug.Log("Player turn started. Energy refreshed.");
    }

    public void EndPlayerTurn()
    {
        currentState = TurnState.EnemyTurn;
        
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