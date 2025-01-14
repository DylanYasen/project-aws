using System.Linq;
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
    public DeckManager deckManager { get; private set; }
    public StatusEffectManager statusEffectManager { get; private set; }
    public MysteryEventManager mysteryEventManager { get; private set; }


#if UNITY_EDITOR
    public Encounter overrideEncounter;
#endif

    [Header("UI Prefab")]
    public GameObject combatStatsUIPrefab;

    // Add gold property with event
    public int Gold { get; private set; }
    public event System.Action<int> OnGoldChanged;

    public int traveledNodeCount = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Gold = 0; // Starting gold amount
        turnManager = new TurnManager();
        combatManager = new CombatManager();
        encounterManager = new EncounterManager();
        trinketManager = new TrinketManager();
        deckManager = new DeckManager();
        statusEffectManager = new StatusEffectManager();
        mysteryEventManager = new MysteryEventManager();
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            foreach (var enemy in encounterManager.enemies)
            {
                enemy.Die();
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Player.Instance.Die();
        }
    }
#endif
    void Start()
    {
        // @todo: temp, streamline resource loading
        encounterManager.LoadResources();
        trinketManager.LoadResources();
        deckManager.LoadResources();
        mysteryEventManager.LoadResources();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");

        switch (scene.name)
        {
            case "MapScene":
                Player.Instance.SetVisible(false);
                break;
            case "CombatScene":
                Player.Instance.SetVisible(true);

#if UNITY_EDITOR
                encounterManager.SpawnEncounter(overrideEncounter);
#else
                encounterManager.SpawnEncounter();
#endif
                DeckManager.Instance.InitForCombat();
                TurnManager.Instance.Init();
                break;
            case "RestSiteScene":
                break;
            case "Menu":
                break;
            case "Mystery":
                {
                    MysteryEvent mysteryEvent = mysteryEventManager.GetRandomEvent();
                    if (mysteryEvent == null)
                    {
                        Debug.LogError("No mystery event found.");
                        return;
                    }
                    Debug.Log($"Mystery event: {mysteryEvent.eventName}");
                    mysteryEventManager.StartEvent(mysteryEvent);
                }
                break;
            case "Treasure":
                {
                    //  60% chance to get an epic trinket
                    //  40% chance to get a legendary trinket
                    const float epicChance = 0.6f;
                    var rarity = Random.value < epicChance ? TrinketRarity.Epic : TrinketRarity.Legendary;

                    // re-roll if play already has the same trinket
                    Trinket trinket = trinketManager.GetRandomTrinket(rarity);
                    while (trinketManager.playerTrinkets.Any(t => t.name == trinket.name))
                    {
                        trinket = trinketManager.GetRandomTrinket(rarity);
                    }

                    Debug.Log($"Treasure: {trinket.name} ({trinket.rarity})");
                    TreasureUI.Instance.Present(trinket);
                }
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
        traveledNodeCount++;
    }

    public void StartCombatEncounter(Map.NodeType nodeType)
    {
        Debug.Log($"Encounter started! {nodeType}");

        encounterManager.currentEncounterNodeType = nodeType;

        SceneManager.LoadScene("CombatScene");
    }

    public void OnCombatEncounterEnd()
    {
        if (encounterManager.currentEncounter.encounterNodeType == Map.NodeType.Boss)
        {
            CombatSceneUIReferences.Instance.gameWonUI.gameObject.SetActive(true);
        }
        else
        {
            GenerateLoot();

            Player.Instance.EndCombat();
        }

    }

    public void StartRestSite()
    {
        Debug.Log("Starting rest site encounter.");
        SceneManager.LoadScene("RestSite");
    }

    public void StartTreasure()
    {
        Debug.Log("Starting treasure encounter.");
        SceneManager.LoadScene("Treasure");
    }

    public void StartShop()
    {
        Debug.Log("Starting shop encounter.");
        SceneManager.LoadScene("Shop");
    }

    public void StartMysteryEvent()
    {
        Debug.Log("Starting mystery event.");
        SceneManager.LoadScene("Mystery");
    }

    public void GameOver()
    {
        StopAllCoroutines(); // enemy turn coroutine is running on GM
        turnManager.currentState = TurnState.EndOfRound;
        encounterManager.enemies.Clear();
        encounterManager.currentEncounter = null;
        CombatSceneUIReferences.Instance.gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        // @todo: kinda weird to clear map here but it's a quick workaround
        PlayerPrefs.DeleteKey("Map");
        SceneManager.LoadScene("Menu");
        traveledNodeCount = -1;
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

    public void MaintanceComplete()
    {
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
