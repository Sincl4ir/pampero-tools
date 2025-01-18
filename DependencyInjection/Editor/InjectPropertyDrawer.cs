using Pampero.Tools.DependencyInjection;
using UnityEditor;
using UnityEngine;

namespace Pampero.Tools.Editor.DependencyInjection
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(InjectAttribute))]
    public class InjectPropertyDrawer : PropertyDrawer 
    {
        private const string IMAGE_PATH = "Assets/PamperoTools/DependencyInjection/Editor/icon.png";
        private Texture2D _icon;

        private Texture2D LoadIcon() 
        {
            if (_icon == null) 
            {
                _icon = AssetDatabase.LoadAssetAtPath<Texture2D>(IMAGE_PATH);
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
#endif
}
//EOF.