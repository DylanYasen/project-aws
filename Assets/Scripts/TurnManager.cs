using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    EnemyTurn,
    EndOfRound
}

public class TurnManager
{
    public static TurnManager Instance { get; private set; }

    private TurnState currentState;

    public TurnManager()
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
        Debug.Log("Player turn started.");
    }

    public void EndPlayerTurn()
    {
        Debug.Log("end player turn");
        if (currentState != TurnState.PlayerTurn)
        {
            Debug.Log("EndPlayerTurn called but not player turn.");
            return;
        }

        currentState = TurnState.EnemyTurn;

        Debug.Log("Player turn ended.");
        // // Discard remaining hand
        // foreach (Card card in DeckManager.Instance.hand)
        // {
        //     DeckManager.Instance.discardPile.Add(card);
        // }
        // DeckManager.Instance.hand.Clear();

        GameManager.Instance.StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy turn started.");

        yield return new WaitForSeconds(1.0f);

        foreach (Enemy enemy in EncounterManager.Instance.enemies)
        {
            enemy.StartTurn();
            yield return enemy.PlayTurn();
        }

        yield return new WaitForSeconds(1.0f);

        StartPlayerTurn();
    }
}