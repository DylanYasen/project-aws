using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EncounterManager
{
    public static EncounterManager Instance { get; private set; }

    public List<Enemy> enemies = new();
    public Dictionary<Map.NodeType, List<Encounter>> nodeTypeToEncounters = new();

    public Map.NodeType currentEncounterNodeType;

    public Encounter currentEncounter;

    public EncounterManager()
    {
        Instance = this;
    }

    public async void LoadResources()
    {
        var encounterLoadHandle = Addressables.LoadAssetsAsync<Encounter>("Encounters", encounter =>
        {
            Debug.Log($"Loaded encounter: {encounter.name}");

            if (!nodeTypeToEncounters.ContainsKey(encounter.encounterNodeType))
            {
                nodeTypeToEncounters[encounter.encounterNodeType] = new List<Encounter>();
            }
            nodeTypeToEncounters[encounter.encounterNodeType].Add(encounter);
        });

        await encounterLoadHandle.Task;

        if (encounterLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successfully loaded all encounters");
        }
        else
        {
            Debug.LogError("Failed to load encounters");
        }
    }

    public void SpawnEncounter()
    {
        var encounters = nodeTypeToEncounters[currentEncounterNodeType];

        var eligibleEncounters = encounters.Where(encounter => encounter.minRequiredLevel <= GameManager.Instance.traveledNodeCount).ToList();

        var encounter = eligibleEncounters[Random.Range(0, eligibleEncounters.Count)];

        currentEncounter = encounter;

        SpawnEnemies(encounter);
    }

    private void SpawnEnemies(Encounter encounter)
    {
        int enemyCount = encounter.enemyPrefabs.Count;
        float screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).x; // Start from middle

        // Calculate spacing
        float totalWidth = screenRightEdge - screenLeftEdge;
        float spacing = totalWidth / (enemyCount + 1); // +1 for margins

        for (int i = 0; i < enemyCount; i++)
        {
            var enemyPrefab = encounter.enemyPrefabs[i];
            float xPos = screenLeftEdge + spacing * (i + 1); // +1 to start after first margin
            Vector3 spawnPos = new Vector3(xPos, 0, 0);

            var enemyObj = GameObject.Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            var enemy = enemyObj.GetComponent<Enemy>();
            enemy.StartCombat();
            enemies.Add(enemy);
        }
        Player.Instance.StartCombat();
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            Debug.Log("Encounter cleared");

            GameManager.Instance.OnCombatEncounterEnd();
        }
    }
}
