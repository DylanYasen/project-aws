using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TrinketManager
{
    public static TrinketManager Instance { get; private set; }

    public Dictionary<TrinketRarity, List<Trinket>> availableTrinkets = new();

    // Add this new property to store player's trinkets
    public List<Trinket> playerTrinkets { get; private set; } = new List<Trinket>();

    public TrinketManager()
    {
        Instance = this;
        playerTrinkets = new List<Trinket>();
    }

    // Add these new methods to manage player trinkets
    public void AddTrinketToPlayer(Trinket trinket)
    {
        if (trinket != null)
        {
            playerTrinkets.Add(trinket);
            trinket.behavior?.OnAcquired();
            Debug.Log($"Added {trinket.name} to player's collection");
        }
    }

    public void RemoveTrinketFromPlayer(Trinket trinket)
    {
        if (trinket != null && playerTrinkets.Contains(trinket))
        {
            playerTrinkets.Remove(trinket);
            Debug.Log($"Removed {trinket.name} from player's collection");
        }
    }

    public async void LoadResources()
    {
        var trinketLoadHandle = Addressables.LoadAssetsAsync<Trinket>("Trinkets", trinket =>
        {
            Debug.Log($"Loaded trinket: {trinket.name}");
            if (!availableTrinkets.ContainsKey(trinket.rarity))
            {
                availableTrinkets[trinket.rarity] = new List<Trinket>();
            }
            availableTrinkets[trinket.rarity].Add(trinket);
        });

        await trinketLoadHandle.Task;

        if (trinketLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successfully loaded all trinkets");
        }
        else
        {
            Debug.LogError("Failed to load trinkets");
        }
    }

    public Trinket GetRandomTrinket()
    {
        // First roll for rarity
        float rarityRoll = Random.value;
        TrinketRarity selectedRarity;

        // Current distribution:
        // Common: 60% (0.0 - 0.6)
        // Rare: 25% (0.6 - 0.85) 
        // Epic: 10% (0.85 - 0.95)
        // Legendary: 5% (0.95 - 1.0)
        if (rarityRoll < 0.6f)
        {
            selectedRarity = TrinketRarity.Common;
        }
        else if (rarityRoll < 0.85f)
        {
            selectedRarity = TrinketRarity.Rare;
        }
        else if (rarityRoll < 0.95f)
        {
            selectedRarity = TrinketRarity.Epic;
        }
        else
        {
            selectedRarity = TrinketRarity.Legendary;
        }

        // Get trinkets of selected rarity from dictionary
        if (availableTrinkets.TryGetValue(selectedRarity, out List<Trinket> trinketPool) && trinketPool.Count > 0)
        {
            return trinketPool[Random.Range(0, trinketPool.Count)];
        }

        // If no trinkets found for selected rarity, fall back to Common
        if (availableTrinkets.TryGetValue(TrinketRarity.Common, out trinketPool) && trinketPool.Count > 0)
        {
            Debug.LogWarning($"No trinkets found for {selectedRarity} rarity, falling back to Common");
            return trinketPool[Random.Range(0, trinketPool.Count)];
        }

        Debug.LogError("No trinkets available at all!");
        return null;
    }

    public Trinket GetRandomTrinket(TrinketRarity rarity)
    {
        // Get trinkets of specified rarity from dictionary
        if (availableTrinkets.TryGetValue(rarity, out List<Trinket> trinketPool) && trinketPool.Count > 0)
        {
            return trinketPool[Random.Range(0, trinketPool.Count)];
        }

        // If no trinkets found for specified rarity, fall back to Common
        if (availableTrinkets.TryGetValue(TrinketRarity.Common, out trinketPool) && trinketPool.Count > 0)
        {
            Debug.LogWarning($"No trinkets found for {rarity} rarity, falling back to Common");
            return trinketPool[Random.Range(0, trinketPool.Count)];
        }

        Debug.LogError("No trinkets available at all!");
        return null;
    }
}
