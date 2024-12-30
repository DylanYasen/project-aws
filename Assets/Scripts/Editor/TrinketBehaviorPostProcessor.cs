using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class TrinketBehaviorPostProcessor : AssetPostprocessor
{
    private const string TRINKET_PATH = "Assets/Trinkets/Behavior";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            if (!assetPath.EndsWith(".cs")) continue;

            var className = Path.GetFileNameWithoutExtension(assetPath);
            if (!className.StartsWith("TrinketBehavior_")) continue;

            // Get the type from the class name
            var type = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == className);

            if (type == null || !type.IsSubclassOf(typeof(TrinketBehavior))) continue;

            // Create the asset if it doesn't exist
            var assetName = className.Replace("TrinketBehavior_", "");
            var fullPath = $"{TRINKET_PATH}/{assetName}.asset";

            if (!Directory.Exists(TRINKET_PATH))
            {
                Directory.CreateDirectory(TRINKET_PATH);
            }

            if (!File.Exists(fullPath))
            {
                var instance = ScriptableObject.CreateInstance(type);
                AssetDatabase.CreateAsset(instance, fullPath);
                AssetDatabase.SaveAssets();
                Debug.Log($"Created trinket behavior asset: {fullPath}");
            }
        }
    }
} 