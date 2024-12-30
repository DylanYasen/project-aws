using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    StartOfRound,
    PlayerTurn,
    EnemyTurn,
    EndOfRound
}

public class TurnManager
{
    public static TurnManager Instance { get; private set; }

    public TurnState currentState;

    // Add events for turn changes
    public event Action OnPlayerTurnStart;
    public event Action OnPlayerTurnEnd;
    public event Action OnEnemyTurnStart;
    public event Action OnEnemyTurnEnd;
    public event Action OnCombatStart;

    public TurnManager()
    {
        if (Instance == null) Instance = this;
    }

    public void Init()
    {
        currentState = TurnState.StartOfRound;
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        if (currentState == TurnState.EndOfRound)
        {
            Debug.Log("end of round");
            return;
        }

        if (currentState == TurnState.StartOfRound)
        {
            OnCombatStart?.Invoke();
        }

        currentState = TurnState.PlayerTurn;
        OnPlayerTurnStart?.Invoke();


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
        OnPlayerTurnEnd?.Invoke();

        currentState = TurnState.EnemyTurn;
        OnEnemyTurnStart?.Invoke();

        Debug.Log("Player turn ended.");
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

        OnEnemyTurnEnd?.Invoke();
        StartPlayerTurn();
    }
}