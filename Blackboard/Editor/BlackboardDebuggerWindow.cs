#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Pampero.Blackboard
{
    public class BlackboardDebuggerWindow : EditorWindow
    {
        private GameObject _selectedObject;
        private BlackboardComponent _blackboardComponent;
        private Dictionary<string, IBlackboardVariable> _variables;
        private Vector2 _scrollPosition;

        [MenuItem("Pampero/Tools/Blackboard Debugger")]
        public static void ShowWindow()
        {
            var window = GetWindow<BlackboardDebuggerWindow>("Blackboard Debugger");
            window.autoRepaintOnSceneChange = true;
        }

        private void OnEnable()
        {
            EditorApplication.update += Repaint;
        }

        private void OnDisable()
        {
            EditorApplication.update -= Repaint;
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Select a GameObject with a BlackboardComponent", EditorStyles.boldLabel);
            _selectedObject = Selection.activeGameObject;

            if (_selectedObject == null)
            {
                EditorGUILayout.HelpBox("No GameObject selected.", MessageType.Info);
                return;
            }

            if (!_selectedObject.TryGetComponent<BlackboardComponent>(out _blackboardComponent) || _blackboardComponent == null)
            {
                EditorGUILayout.HelpBox("Selected GameObject does not have a BlackboardComponent.", MessageType.Warning);
                return;
            }

            _variables = GetVariablesDictionary(_blackboardComponent.Blackboard);
            if (_variables == null)
            {
                EditorGUILayout.HelpBox("Could not access Blackboard variables.", MessageType.Error);
                return;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Blackboard Variables", EditorStyles.boldLabel);

            // Begin scroll view here
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            foreach (var kvp in _variables)
            {
                string key = kvp.Key;
                IBlackboardVariable variable = kvp.Value;
                object value = variable?.GetValue();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField(key, GUILayout.Width(150));

                if (value is UnityEngine.Object unityObj)
                {
                    EditorGUILayout.ObjectField(unityObj, unityObj.GetType(), true);
                }
                else
                {
                    EditorGUILayout.LabelField(value?.ToString() ?? "null");
                }
                EditorGUILayout.EndHorizontal();

                // Show MonoBehaviour fields (optional)
                if (value is MonoBehaviour mb)
                {
                    var fields = mb.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                    EditorGUI.indentLevel++;
                    foreach (var field in fields)
                    {
                        var fieldVal = field.GetValue(mb);
                        EditorGUILayout.LabelField(field.Name, fieldVal?.ToString() ?? "null");
                    }
                    EditorGUI.indentLevel--;
                }
            }

            // End scroll view here
            EditorGUILayout.EndScrollView();
        }

        private Dictionary<string, IBlackboardVariable> GetVariablesDictionary(Blackboard blackboard)
        {
            if (blackboard == null) return null;

            var field = typeof(Blackboard).GetField("_variables", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(blackboard) as Dictionary<string, IBlackboardVariable>;
        }
    }
}
//EOF.
#endif