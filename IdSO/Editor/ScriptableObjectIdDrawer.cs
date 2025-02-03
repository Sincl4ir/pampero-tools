using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pampero.Tools.IdSO.Editor
{
#if UNITY_EDITOR
    /// <summary>
    /// Custom drawer for the ScriptableObject ID attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
    public class ScriptableObjectIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
            }

            var propertyRect = new Rect(position);
            propertyRect.xMax -= 100;
            var buttonRect = new Rect(position);
            buttonRect.xMin = position.xMax - 100;

            GUI.enabled = false;
            EditorGUI.PropertyField(propertyRect, property, label, true);
            GUI.enabled = true;

            if (GUI.Button(buttonRect, "Regenerate ID"))
            {
                property.stringValue = Guid.NewGuid().ToString();
            }
        }
    }
#endif
}
//EOF.