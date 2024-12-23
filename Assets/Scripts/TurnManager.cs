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

    public Enemy[] enemies;

    private TurnState currentState;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void Init()
    {
        StartPlayerTurn();


        // @todo: hook this up to encounter spwaning
        {
            enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        }
    }

    public void StartPlayerTurn()
    {
        currentState = TurnState.PlayerTurn;

        Player.Instance.StartTurn();
        Debug.Log("Player turn started.");
    }

    public void EndPlayerTurn()
    {
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

        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy turn started.");

        yield return new WaitForSeconds(1.0f);

        foreach (Enemy enemy in enemies)
        {
            enemy.StartTurn();
            yield return enemy.PlayTurn();
        }

        yield return new WaitForSeconds(1.0f);

        StartPlayerTurn();
    }
}