using UnityEngine;

public class CombatSceneUIReferences : MonoBehaviour
{
    public static CombatSceneUIReferences Instance { get; private set; }

    public Transform handArea;
    public GameObject cardPrefab;
    public GameObject gameOverUI;
    public EnergyBarUI energyBar;
    public LootUI lootUI;

    private void Awake()
    {
        Instance = this;
    }
}
