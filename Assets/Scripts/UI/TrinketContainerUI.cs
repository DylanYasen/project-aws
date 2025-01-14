using UnityEngine;
using System.Collections.Generic;

public class TrinketContainerUI : MonoBehaviour
{
    [SerializeField] private GameObject trinketUIPrefab;

    private List<TrinketUI> trinketUIs = new List<TrinketUI>();

    private void Start()
    {
        InitializeTrinkets();
    }

    private void InitializeTrinkets()
    {
        // @todo: pool
        foreach (var trinket in TrinketManager.Instance.playerTrinkets)
        {
            GameObject trinketUIObj = Instantiate(trinketUIPrefab, transform);
            TrinketUI trinketUI = trinketUIObj.GetComponent<TrinketUI>();
            trinketUI.Initialize(trinket);
            trinketUIs.Add(trinketUI);
        }
    }
}