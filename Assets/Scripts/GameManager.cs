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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");

        switch (scene.name)
        {
            case "MapScene":
                break;
            case "CombatScene":
                DeckManager.Instance.Init();
                TurnManager.Instance.Init();
                break;
            case "RestSiteScene":
                break;
            case "Menu":
                break;
            default:
                Debug.Log("Unknown scene loaded.");
                break;
        }
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

    public void StartCombatEncounter()
    {
        Debug.Log("Encounter started!");

        SceneManager.LoadScene("CombatScene"); // @todo: fix hardcode
    }

    public void OnCombatEncounterEnd()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void StartRestSite()
    {
        Debug.Log("Starting rest site encounter.");
        // Open rest site UI and allow player to heal or upgrade
    }

    public void StartTreasure()
    {
        Debug.Log("Starting treasure encounter.");
        // Reward the player with a relic or powerful card
    }

    public void StartStore()
    {
        Debug.Log("Starting store encounter.");
        // Open the store UI for purchasing items
    }

    public void StartMysteryEvent()
    {
        Debug.Log("Starting mystery event.");
        // Trigger a random event with unpredictable outcomes
    }
}