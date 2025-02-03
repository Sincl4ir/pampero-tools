using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pampero.Tools.IdSO
{
#if UNITY_EDITOR
    /// <summary>
    /// Checks for duplicate ScriptableObject IDs before building.
    /// </summary>
    public static class ScriptableObjectChecker
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void CheckIds()
        {
            var ids = new HashSet<string>();

            var guids = AssetDatabase.FindAssets("t:" + typeof(ScriptableObjectWithId));
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var so = AssetDatabase.LoadAssetAtPath<ScriptableObjectWithId>(path);
                if (string.IsNullOrEmpty(so.Id))
                {
                    Debug.LogError("ScriptableObject doesn't have ID", so);
                    throw new Exception();
                }
                if (!ids.Add(so.Id))
                {
                    Debug.LogError($"ScriptableObject has the same ID as some other SO: {so}", so);
                    throw new Exception();
                }
            }
        }
    }
#endif
}
//EOF.