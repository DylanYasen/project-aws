using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Linq;

public class CardDataDumper : EditorWindow
{
    [MenuItem("Tools/Card Data/Dump Card Info to Markdown")]
    public static void DumpCardInfo()
    {
        // Find all Card scriptable objects in the project
        string[] guids = AssetDatabase.FindAssets("t:Card");
        
        StringBuilder sb = new StringBuilder();
        
        // Create table header
        sb.AppendLine("| Name | Description | Cost | Price |");
        sb.AppendLine("|------|-------------|------|-------|");
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Card card = AssetDatabase.LoadAssetAtPath<Card>(path);
            
            if (card != null)
            {
                // Escape any pipe characters in the description to prevent breaking the table format
                string escapedDescription = card.description.Replace("|", "\\|");
                
                // Add row for each card
                sb.AppendLine($"| {card.cardName} | {escapedDescription} | {card.cost} | {card.price} |");
            }
        }
        
        // Save to file
        string outputPath = Application.dataPath + "/../CardData.md";
        File.WriteAllText(outputPath, sb.ToString());
        
        Debug.Log($"Card data has been dumped to: {outputPath}");
        
        // Refresh the asset database to show the new file
        AssetDatabase.Refresh();
    }
} 