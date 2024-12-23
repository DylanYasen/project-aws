using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CombatManager combatManager { get; private set; }

    [Header("UI Prefab")]
    public GameObject combatStatsUIPrefab;


    private void Awake()
    {
        Instance = this;
        combatManager = new CombatManager();
    }

    void Start()
    {
        DeckManager.Instance.Init();
        TurnManager.Instance.Init();
    }

    public void EndTurn()
    {
        // energy = 3;
        Debug.Log("Turn ended. Energy reset to 3.");
    }

}