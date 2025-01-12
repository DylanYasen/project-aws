using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Reflection;

[CustomEditor(typeof(MysteryEvent))]
public class MysteryEventEditor : Editor
{
    private void DrawEffectProperties(SerializedProperty effectRef)
    {
        if (effectRef.managedReferenceValue == null) return;

        var effectType = effectRef.managedReferenceValue.GetType();
        
        EditorGUILayout.Space(5);
        EditorGUI.indentLevel++;

        var fields = effectType.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(field => field.IsPublic || field.GetCustomAttribute<SerializeField>() != null)
            .Where(field => field.GetCustomAttribute<HideInInspector>() == null);

        foreach (var field in fields)
        {
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

    private void DrawChoiceProperties(SerializedProperty choiceProperty)
    {
        // Draw default properties
        var choiceText = choiceProperty.FindPropertyRelative("choiceText");
        var resultText = choiceProperty.FindPropertyRelative("resultText");
        var effects = choiceProperty.FindPropertyRelative("effects");

        EditorGUILayout.PropertyField(choiceText);
        EditorGUILayout.PropertyField(resultText);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

        // Add effect button
        if (GUILayout.Button("Add Effect"))
        {
            var menu = new GenericMenu();
            
            var effectTypes = TypeCache.GetTypesDerivedFrom<StatusEffect>()
                .Where(t => !t.IsAbstract);

            foreach (var type in effectTypes)
            {
                menu.AddItem(new GUIContent(type.Name), false, () =>
                {
                    var instance = Activator.CreateInstance(type);
                    effects.InsertArrayElementAtIndex(effects.arraySize);
                    var element = effects.GetArrayElementAtIndex(effects.arraySize - 1);
                    element.managedReferenceValue = instance;
                    serializedObject.ApplyModifiedProperties();
                });
            }
            
            menu.ShowAsContext();
        }

        EditorGUI.indentLevel++;
        // Draw each effect
        for (int i = 0; i < effects.arraySize; i++)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            var effectProperty = effects.GetArrayElementAtIndex(i);
            
            // Show effect type and remove button
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Effect: {effectProperty.managedReferenceValue?.GetType().Name ?? "None"}");
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                effects.DeleteArrayElementAtIndex(i);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                continue;
            }
            EditorGUILayout.EndHorizontal();

            if (effectProperty.managedReferenceValue != null)
            {
                DrawEffectProperties(effectProperty);
            }

            EditorGUILayout.EndVertical();
        }
        EditorGUI.indentLevel--;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw basic properties
        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventImage"));

        // Draw choices array header
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("choices"), new GUIContent("Choices"), false);
        
        if (serializedObject.FindProperty("choices").isExpanded)
        {
            EditorGUI.indentLevel++;
            SerializedProperty choicesArray = serializedObject.FindProperty("choices");
            
            // Add choice button
            if (GUILayout.Button("Add Choice"))
            {
                choicesArray.arraySize++;
            }

            // Draw each choice
            for (int i = 0; i < choicesArray.arraySize; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Choice {i + 1}", EditorStyles.boldLabel);
                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    choicesArray.DeleteArrayElementAtIndex(i);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    continue;
                }
                EditorGUILayout.EndHorizontal();

                DrawChoiceProperties(choicesArray.GetArrayElementAtIndex(i));
                
                EditorGUILayout.EndVertical();
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
} 