using UnityEngine;

public class CombatSceneUIReferences : MonoBehaviour
{
    public static CombatSceneUIReferences Instance { get; private set; }

    public Transform handArea;
    public GameObject cardPrefab;
    public GameObject gameOverUI;
    public EnergyBarUI energyBar;
    public LootUI lootUI;
    public UIGameWon gameWonUI;

    private void Awake()
    {
        Instance = this;
    }
}
