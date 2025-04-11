using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SpawnableObject))]
public class SpawnableObjectDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 6 + 10;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight + 2;
        Rect rect = new Rect(position.x, position.y, position.width, lineHeight);

        EditorGUI.LabelField(rect, label, EditorStyles.boldLabel);
        rect.y += lineHeight;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("prefab"));
        rect.y += lineHeight;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("spawnOnlyOnce"));
        rect.y += lineHeight;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("maxObjectsOnScreen"));
        rect.y += lineHeight;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("rotation"));
        rect.y += lineHeight;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("priority"));
        rect.y += lineHeight;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("respawnDelay"));

        EditorGUI.EndProperty();
    }
}