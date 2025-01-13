using UnityEngine;
using System.Collections.Generic;
using Map;


[CreateAssetMenu(fileName = "New Encounter", menuName = "Encounter")]
public class Encounter : ScriptableObject
{
    public NodeType encounterNodeType;

    [Tooltip("Player needs to be at least passed this number of nodes to unlock this encounter")]
    public int minRequiredLevel;

    [Header("Enemies")]
    public List<GameObject> enemyPrefabs = new();

    [Header("Rewards")]
    public int goldReward;

}