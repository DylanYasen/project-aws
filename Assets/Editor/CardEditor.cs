using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Reflection;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    private void DrawEffectProperties(SerializedProperty effectRef, Type effectType)
    {
        if (effectType == null) return;

        // Get all fields that should be shown in inspector
        var fields = effectType.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(field => field.IsPublic || field.GetCustomAttribute<SerializeField>() != null)
            .Where(field => field.GetCustomAttribute<HideInInspector>() == null);

        // Get all properties that should be shown in inspector
        var properties = effectType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => prop.GetCustomAttribute<SerializeField>() != null)
            .Where(prop => prop.GetCustomAttribute<HideInInspector>() == null);

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Effect Properties", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

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

        foreach (var property in properties)
        {
            if (property.CanRead && property.CanWrite)
            {
                SerializedProperty prop = effectRef.FindPropertyRelative(property.Name);
                if (prop != null)
                {
                    string niceName = ObjectNames.NicifyVariableName(property.Name);
                    EditorGUILayout.PropertyField(prop, new GUIContent(niceName));
                }
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