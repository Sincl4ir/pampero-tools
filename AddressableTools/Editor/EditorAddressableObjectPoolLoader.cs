#if UNITY_EDITOR
using Pampero.Tools.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Pampero.Tools.Pooling.Addressables.Editor {
    public class EditorAddressableObjectPoolLoader {
        const string PRECOMPILER_FLAG = "ADDRESSABLE_ASSETS";
        const string SKIP_AA_FLAG = "SKIP_AA";

        [MenuItem("E404/AddressableAssetsObjectPool/Enable")]
        static void AddPreCompiler()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> defines = definesString.Split(';').ToList();

            foreach (string define in defines)
            {
                if (define == PRECOMPILER_FLAG)
                    return;
            }

            defines.Add(PRECOMPILER_FLAG);

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", defines));
        }

        [MenuItem("E404/AddressableAssetsObjectPool/Skip AA")]
        static void SkipPreCompiler()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> defines = definesString.Split(';').ToList();

            foreach (string define in defines)
            {
                if (define == SKIP_AA_FLAG)
                    return;
            }

            defines.Add(SKIP_AA_FLAG);

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", defines));
        }

        [MenuItem("E404/AddressableAssetsObjectPool/Force AA")]
        static void ForcePreCompiler()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> defines = definesString.Split(';').ToList();

            foreach (string define in defines)
            {
                if (define == SKIP_AA_FLAG)
                {
                    defines.Remove(SKIP_AA_FLAG);
                    break;
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", defines));
        }

        [MenuItem("E404/AddressableAssetsObjectPool/Disable")]
        static void RemovePreCompiler()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> defines = definesString.Split(';').ToList();

            foreach (string define in defines)
            {
                if (define == PRECOMPILER_FLAG)
                {
                    defines.Remove(PRECOMPILER_FLAG);
                    break;
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", defines));
        }

#if ADDRESSABLE_ASSETS
        [MenuItem("E404/AddressableAssetsObjectPool/Create AddressableObjectPool")]
        static void CreateObjectPoolManager()
        {
            var instance = UnityEngine.GameObject.FindObjectOfType<AddressableObjectPool>();
            if(instance != null)
            {
                UnityEngine.Debug.Log("There is already a AddressableObjectPool in scene. Skipping creation");
                Selection.activeGameObject = instance.gameObject;
                return;
            }

            var go = new UnityEngine.GameObject("AddressableObjectPool");
            go.AddComponent<AddressableObjectPool>();
            go.AddComponent<DontDestroyOnLoadGameObject>();
            Selection.activeGameObject = go;
        }

        [MenuItem("E404/AddressableAssetsObjectPool/Create AddressableParticlePool")]
        static void CreateAddressableParticlePoolManager()
        {
            var instance = UnityEngine.GameObject.FindObjectOfType<AddressableParticlePool>();
            if (instance != null)
            {
                UnityEngine.Debug.Log("There is already a AddressableObjectPool in scene. Skipping creation");
                Selection.activeGameObject = instance.gameObject;
                return;
            }

            var go = new UnityEngine.GameObject("AddressableObjectPool");
            go.AddComponent<AddressableParticlePool>();
            go.AddComponent<DontDestroyOnLoadGameObject>();
            go.AddComponent<AddressablePoolCreator>();
            Selection.activeGameObject = go;
        }
#endif
    }

}
#endif