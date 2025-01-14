using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(Unit), true)]
public class UnitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Unit unit = (Unit)target;
        
        // Only show the display name button for Enemy components
        if (unit is Enemy enemy)
        {
            EditorGUILayout.Space();
            if (GUILayout.Button("Set Display Name from GameObject"))
            {
                string objName = enemy.gameObject.name;
                
                // Split on dash and take the last part
                int dashIndex = objName.LastIndexOf('-');
                if (dashIndex >= 0)
                {
                    enemy.displayName = objName.Substring(dashIndex + 1).Trim();
                }
                else
                {
                    enemy.displayName = objName;
                }
                
                // Mark the object as dirty to ensure Unity saves the change
                EditorUtility.SetDirty(enemy);
            }
        }

        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField($"Type: {unit.GetType().Name}");
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animation Testing", EditorStyles.boldLabel);

        if (Application.isPlaying)
        {
            // Get all animation types from the enum
            var animationTypes = Enum.GetValues(typeof(Unit.UnitAnimationType))
                                   .Cast<Unit.UnitAnimationType>()
                                   .ToList();

            // Create buttons in pairs
            for (int i = 0; i < animationTypes.Count; i += 2)
            {
                EditorGUILayout.BeginHorizontal();
                
                // First button in pair
                if (GUILayout.Button($"Test {animationTypes[i]}"))
                {
                    unit.Animate(animationTypes[i]);
                }
                
                // Second button in pair (if exists)
                if (i + 1 < animationTypes.Count)
                {
                    if (GUILayout.Button($"Test {animationTypes[i + 1]}"))
                    {
                        unit.Animate(animationTypes[i + 1]);
                    }
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Enter Play Mode to test animations", MessageType.Info);
        }
    }
} 