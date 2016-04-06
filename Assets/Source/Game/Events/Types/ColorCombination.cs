using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ColorCombination {
    public List<EButton.ColorType> colors = new List<EButton.ColorType>();
    public int amount = 3;

    public EButton.ColorType this[int key]
    {
        get
        {
            return colors[key];
        }
        set
        {
            colors[key] = value;
        }
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ColorCombination))]
public class IngredientDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        SerializedProperty colors = property.FindPropertyRelative("colors");

        if (colors.arraySize < property.FindPropertyRelative("amount").intValue) {
            colors.InsertArrayElementAtIndex(colors.arraySize);
        }

        for (int i = 0, len = colors.arraySize; i < len; i++)
        {
            Rect colorPos = new Rect(position.x + i * (position.width/len), position.y, position.width / len, position.height);
            EditorGUI.PropertyField(colorPos, colors.GetArrayElementAtIndex(i), GUIContent.none);
        }

        EditorGUI.EndProperty();
    }
}
#endif