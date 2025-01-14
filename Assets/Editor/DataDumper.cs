using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Linq;

public class DataDumper : EditorWindow
{
    [MenuItem("Tools/Data/Dump Card Info to Markdown")]
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

    [MenuItem("Tools/Data/Dump Trinket Info to Markdown")]
    public static void DumpTrinketInfo()
    {
        // Find all Trinket scriptable objects in the project
        string[] guids = AssetDatabase.FindAssets("t:Trinket");

        StringBuilder sb = new StringBuilder();

        // Create table header
        sb.AppendLine("| Name | Description | Price |");
        sb.AppendLine("|------|-------------|-------|");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Trinket trinket = AssetDatabase.LoadAssetAtPath<Trinket>(path);

            if (trinket != null)
            {
                // Escape any pipe characters in the description to prevent breaking the table format
                string escapedDescription = trinket.description.Replace("|", "\\|");

                // Add row for each trinket
                sb.AppendLine($"| {trinket.displayName} | {escapedDescription} | {trinket.price} |");
            }
        }

        // Save to file
        string outputPath = Application.dataPath + "/../TrinketData.md";
        File.WriteAllText(outputPath, sb.ToString());

        Debug.Log($"Trinket data has been dumped to: {outputPath}");

        // Refresh the asset database to show the new file
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Data/Dump All Data to Markdown")]
    public static void DumpAllData()
    {
        DumpCardInfo();
        DumpTrinketInfo();
        Debug.Log("All data has been dumped to markdown files");
    }
}