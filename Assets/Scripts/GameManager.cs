using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CombatManager combatManager { get; private set; }
    public TurnManager turnManager { get; private set; }
    public EncounterManager encounterManager { get; private set; }
    public TrinketManager trinketManager { get; private set; }

    [Header("UI Prefab")]
    public GameObject combatStatsUIPrefab;

    // Add gold property with event
    public int Gold { get; private set; }
    public event System.Action<int> OnGoldChanged;

    public int PlayerHealth { get; private set; }
    public int PlayerMaxHealth { get; private set; }
    public event System.Action<int, int> OnPlayerHealthChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Gold = 100; // Starting gold amount
        turnManager = new TurnManager();
        combatManager = new CombatManager();
        encounterManager = new EncounterManager();
        trinketManager = new TrinketManager();
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        {
            PlayerHealth = 100;
            PlayerMaxHealth = 100;
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        // @temp: some debug keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartShop();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartRestSite();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCombatEncounter(Map.NodeType.MinorEnemy);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartTreasure();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartMysteryEvent();
        }
    }
#endif
    void Start()
    {
        // @todo: temp, streamline resource loading
        encounterManager.LoadResources();
        trinketManager.LoadResources();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");

        switch (scene.name)
        {
            case "MapScene":
                break;
            case "CombatScene":

                // @temp
                {
                    var trinket = trinketManager.GetRandomTrinket(TrinketRarity.Epic);
                    trinketManager.AddTrinketToPlayer(trinket);
                }
                encounterManager.SpawnEncounter();
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
        EnterMapScene();
    }

    public void EnterMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void StartCombatEncounter(Map.NodeType nodeType)
    {
        Debug.Log("Encounter started!");

        encounterManager.currentEncounterNodeType = nodeType;

        SceneManager.LoadScene("CombatScene"); // @todo: fix hardcode
    }

    public void OnCombatEncounterEnd()
    {
        // @todo: save stuff
        PlayerHealth = Player.Instance.currentHP;

        GenerateLoot();
    }

    public void StartRestSite()
    {
        Debug.Log("Starting rest site encounter.");
        SceneManager.LoadScene("RestSite");
    }

    public void StartTreasure()
    {
        Debug.Log("Starting treasure encounter.");
        // Reward the player with a relic or powerful card
    }

    public void StartShop()
    {
        Debug.Log("Starting shop encounter.");
        SceneManager.LoadScene("Shop");
    }

    public void StartMysteryEvent()
    {
        Debug.Log("Starting mystery event.");
        // Trigger a random event with unpredictable outcomes
    }

    public void GameOver()
    {
        StopAllCoroutines(); // enemy turn coroutine is running on GM
        turnManager.currentState = TurnState.EndOfRound;
        CombatSceneUIReferences.Instance.gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        // @todo: kinda weird to clear map here but it's a quick workaround
        PlayerPrefs.DeleteKey("Map");

        SceneManager.LoadScene("Menu");
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        OnGoldChanged?.Invoke(Gold);
        Debug.Log($"Gold added: {amount}. Total gold: {Gold}");
    }

    public bool SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            OnGoldChanged?.Invoke(Gold);
            return true;
        }
        return false;
    }

    public void ModifyPlayerHealth(int amount)
    {
        PlayerHealth += amount;
        OnPlayerHealthChanged?.Invoke(PlayerHealth, PlayerMaxHealth);
    }

    public void ModifyPlayerMaxHealth(int amount)
    {
        PlayerMaxHealth += amount;
        OnPlayerHealthChanged?.Invoke(PlayerHealth, PlayerMaxHealth);
    }

    public void Maintance()
    {
        int healthToAdd = 10;
        ModifyPlayerHealth(healthToAdd);

        // @todo: add a delay and some juice before loading the map scene

        SceneManager.LoadScene("MapScene");
    }


    void GenerateLoot()
    {
        var encounter = encounterManager.currentEncounter;
        int gold = encounter.goldReward;

        AddGold(gold);

        var lootUI = CombatSceneUIReferences.Instance.lootUI;
        lootUI.gameObject.SetActive(true);
        lootUI.ShowLoot(gold);
    }
}
