using UnityEditor;
using UnityEngine;

namespace Pampero.DependencyInjection
{
    [CustomPropertyDrawer(typeof(InjectAttribute))]
    public class InjectPropertyDrawer : PropertyDrawer 
    {
        private Texture2D _icon;

        private Texture2D LoadIcon() 
        {
            if (_icon == null) 
            {
                _icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Project/Scripts/DependencyInjection/Editor/icon.png");
            }

            return _icon;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            _icon = LoadIcon();
            var iconRect = new Rect(position.x, position.y, 20, 20);
            position.xMin += 24;

            if (_icon != null) 
            {
                var savedColor = GUI.color;
                GUI.color = property.objectReferenceValue == null ? savedColor : Color.green;
                GUI.DrawTexture(iconRect, _icon);
                GUI.color = savedColor;
            }
            
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
//EOF.