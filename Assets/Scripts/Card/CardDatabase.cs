using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Card Database")]
public class CardDatabase : ScriptableObject
{
    public List<Card> allCards;


#if UNITY_EDITOR
    // Automatically populate the card list by finding all Card ScriptableObjects
    [ContextMenu("Populate Database")]
    public void PopulateDatabase()
    {
        allCards = new List<Card>();
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:Card"); // Find all assets of type Card

        foreach (string guid in guids)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            Card card = UnityEditor.AssetDatabase.LoadAssetAtPath<Card>(path);
            if (card != null)
            {
                allCards.Add(card);
            }
        }

        Debug.Log($"Populated database with {allCards.Count} cards.");
    }
#endif

    public Card GetCardByName(string cardName)
    {
        return allCards.FirstOrDefault(card => card.cardName == cardName);
    }
}