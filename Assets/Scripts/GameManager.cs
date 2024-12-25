using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CombatManager combatManager { get; private set; }
    public EncounterManager encounterManager { get; private set; }

    [Header("UI Prefab")]
    public GameObject combatStatsUIPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        combatManager = new CombatManager();
        encounterManager = new EncounterManager();

        DontDestroyOnLoad(gameObject);
    }

    public void StartCombatEncounter()
    {
        DeckManager.Instance.Init();
        TurnManager.Instance.Init();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void EndTurn()
    {
        // energy = 3;
        Debug.Log("Turn ended. Energy reset to 3.");
    }

}