using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureUI : MonoBehaviour
{
    public static TreasureUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private ShopTrinketUI trinketUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Present(Trinket trinket)
    {
        trinketUI.Initialize(trinket, OnTrinketClaimed);
    }

    private void OnTrinketClaimed(ShopTrinketUI trinketUI)
    {
        GameManager.Instance.trinketManager.AddTrinketToPlayer(trinketUI.trinket);
        GameManager.Instance.EnterMapScene();
    }
}
