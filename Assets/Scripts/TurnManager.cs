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

    public TurnState currentState;

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
        if (currentState == TurnState.EndOfRound)
        {
            Debug.Log("end of round");
            return;
        }

        currentState = TurnState.PlayerTurn;

        Player.Instance.StartTurn();
        Debug.Log("Player turn started.");
    }

    public void EndPlayerTurn()
    {

        if (currentState == TurnState.EndOfRound)
        {
            Debug.Log("end of round");
            return;
        }

        if (currentState != TurnState.PlayerTurn)
        {
            Debug.Log("EndPlayerTurn called but not player turn.");
            return;
        }

        Debug.Log("end player turn");

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