using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Reflection;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    private void DrawStatusEffectProperties(SerializedProperty statusEffectRef)
    {
        if (statusEffectRef.managedReferenceValue == null) return;

        var statusEffectType = statusEffectRef.managedReferenceValue.GetType();
        
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Status Effect Properties", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        var fields = statusEffectType.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(field => field.IsPublic || field.GetCustomAttribute<SerializeField>() != null)
            .Where(field => field.GetCustomAttribute<HideInInspector>() == null);

        foreach (var field in fields)
        {
            if (field.Name == "m_Script") continue;
            
            SerializedProperty prop = statusEffectRef.FindPropertyRelative(field.Name);
            if (prop != null)
            {
                string niceName = ObjectNames.NicifyVariableName(field.Name);
                EditorGUILayout.PropertyField(prop, new GUIContent(niceName));
            }
        }

        EditorGUI.indentLevel--;
    }

    private void DrawEffectProperties(SerializedProperty effectRef, Type effectType)
    {
        if (effectType == null) return;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Effect Properties", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        // Special handling for ApplyStatusEffect
        if (effectType == typeof(ApplyStatusEffect))
        {
            var statusEffectRef = effectRef.FindPropertyRelative("statusEffect");
            
            // Get all status effect types
            var statusEffectTypes = TypeCache.GetTypesDerivedFrom<StatusEffect>()
                .Where(t => !t.IsAbstract)
                .ToArray();

            // Create names for popup
            var statusEffectNames = statusEffectTypes.Select(t => t.Name).ToArray();

            // Find current selection
            int currentIndex = -1;
            if (statusEffectRef.managedReferenceValue != null)
            {
                currentIndex = Array.IndexOf(statusEffectTypes, statusEffectRef.managedReferenceValue.GetType());
            }

            // Show dropdown
            int newIndex = EditorGUILayout.Popup("Status Effect Type", currentIndex, statusEffectNames);
            if (newIndex != currentIndex)
            {
                if (newIndex >= 0)
                {
                    var instance = Activator.CreateInstance(statusEffectTypes[newIndex]);
                    statusEffectRef.managedReferenceValue = instance;
                }
                else
                {
                    statusEffectRef.managedReferenceValue = null;
                }
            }

            // Draw status effect properties if one is selected
            if (statusEffectRef.managedReferenceValue != null)
            {
                DrawStatusEffectProperties(statusEffectRef);
            }
        }

        // Draw all other properties
        var fields = effectType.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(field => field.IsPublic || field.GetCustomAttribute<SerializeField>() != null)
            .Where(field => field.GetCustomAttribute<HideInInspector>() == null)
            .Where(field => field.Name != "statusEffect"); // Skip statusEffect as we handle it specially

        foreach (var field in fields)
        {
            // Skip certain Unity-specific fields
            if (field.Name == "m_Script") continue;
            
            SerializedProperty prop = effectRef.FindPropertyRelative(field.Name);
            if (prop != null)
            {
                string niceName = ObjectNames.NicifyVariableName(field.Name);
                EditorGUILayout.PropertyField(prop, new GUIContent(niceName));
            }
        }

        EditorGUI.indentLevel--;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw default inspector for most properties
        DrawPropertiesExcluding(serializedObject, "effects");

        // Get the effects list
        SerializedProperty effectsList = serializedObject.FindProperty("effects");
        
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("Card Effects", EditorStyles.boldLabel);
        
        // Add array size control
        EditorGUI.BeginChangeCheck();
        int newSize = EditorGUILayout.IntField("Size", effectsList.arraySize);
        if (EditorGUI.EndChangeCheck())
        {
            effectsList.arraySize = newSize;
        }

        EditorGUILayout.Space();

        // For each effect entry
        for (int i = 0; i < effectsList.arraySize; i++)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            // Add header with remove button
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Effect {i + 1}", EditorStyles.boldLabel);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                effectsList.DeleteArrayElementAtIndex(i);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                continue;
            }
            EditorGUILayout.EndHorizontal();
            
            SerializedProperty effectEntry = effectsList.GetArrayElementAtIndex(i);
            SerializedProperty effectRef = effectEntry.FindPropertyRelative("cardEffect");

            // Get all effect types
            var effectTypes = TypeCache.GetTypesDerivedFrom<CardEffect>()
                .Where(t => !t.IsAbstract)
                .ToArray();

            // Create names for popup
            var effectNames = effectTypes.Select(t => t.Name).ToArray();

            // Find current selection
            int currentIndex = -1;
            Type currentEffectType = null;
            if (effectRef.managedReferenceValue != null)
            {
                currentEffectType = effectRef.managedReferenceValue.GetType();
                currentIndex = Array.IndexOf(effectTypes, currentEffectType);
            }

            // Show dropdown
            EditorGUI.indentLevel++;
            int newIndex = EditorGUILayout.Popup("Effect Type", currentIndex, effectNames);
            if (newIndex != currentIndex)
            {
                if (newIndex >= 0)
                {
                    var instance = Activator.CreateInstance(effectTypes[newIndex]);
                    effectRef.managedReferenceValue = instance;
                    currentEffectType = effectTypes[newIndex];
                }
                else
                {
                    effectRef.managedReferenceValue = null;
                    currentEffectType = null;
                }
            }

            // Show effect-specific properties using reflection
            if (effectRef.managedReferenceValue != null)
            {
                DrawEffectProperties(effectRef, currentEffectType);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        // Add new effect button
        if (GUILayout.Button("Add Effect"))
        {
            effectsList.arraySize++;
        }

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
} 