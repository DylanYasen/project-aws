using UnityEngine;
using System.Collections.Generic;
using Map;


[CreateAssetMenu(fileName = "New Encounter", menuName = "Encounter")]
public class Encounter : ScriptableObject
{
    public NodeType encounterNodeType;

    [Header("Enemies")]
    public List<GameObject> enemyPrefabs = new();

    [Header("Rewards")]
    public int goldReward;

}