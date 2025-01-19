#if ADDRESSABLE_ASSETS
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Addler.Runtime.Core;
using System.Threading.Tasks;
using Pampero.Tools.Singleton;
using Pampero.Tools.Utils;

namespace Pampero.Tools.Pooling.Addressables {
    public class AddressableObjectPool : Singleton<AddressableObjectPool> {
        [System.Serializable]
        public class AddressableObjectSpawnProperty {
            [SerializeField] public AssetReference Prefab = null;
            [SerializeField] public int PreWarmAmount = 0;
        }

        [SerializeField] List<AddressableObjectSpawnProperty> _SpawnPropertyList = new List<AddressableObjectSpawnProperty>();
        Dictionary<string, AddressablesPool> objectPools = new Dictionary<string, AddressablesPool>();

        void Start()
        {
            foreach (var poolableObject in _SpawnPropertyList) 
            {
                if (poolableObject == null) { continue; }
                if (poolableObject.Prefab == null) { continue; }
                if (poolableObject.Prefab.RuntimeKeyIsValid() == false) { continue; }

                var key = poolableObject.Prefab.RuntimeKey.ToString();
                var pool = TryCreatePool(key);

                if (pool == null)
                {
                    Debug.Log($"Cannot create pool, probably is already made. Skipping {key}");
                    continue;
                }

                pool.BindTo(gameObject);

                if(poolableObject.PreWarmAmount > 0)
#if SKIP_AA
    continue;
#endif
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    pool.WarmupAsync(poolableObject.PreWarmAmount);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        public AddressablesPool TryCreatePool(AssetReference asset)
        {
            if (asset == null) { return null; }
            if (asset.RuntimeKeyIsValid() == false) { return null; }

            return TryCreatePool(asset.RuntimeKey.ToString());
        }

        public AddressablesPool TryCreatePool(string key)
        {
            if (objectPools.ContainsKey(key)) { return null; }
            var pool = new AddressablesPool(key);
            objectPools.Add(key, pool);

            pool.PoolGameObject.transform.SetParent(transform);

            return pool;
        }

        public bool TryRemovePool(AssetReference asset)
        {
            if (asset == null) { return false; }
            if (asset.RuntimeKeyIsValid() == false) { return false; }

            return TryRemovePool(asset.RuntimeKey.ToString());
        }

        public bool TryRemovePool(string key)
        {
            if (objectPools.ContainsKey(key) == false) { return false; }
            var pool = objectPools[key];
            objectPools.Remove(key);

            pool?.Dispose();
            return true;
        }

        public async Task<PooledObjectOperation<GameObject>> TryGetGameObject(AssetReference asset)
        {
            if (asset == null) { return null; }
            if (asset.RuntimeKeyIsValid() == false) { return null; }

            return await TryGetGameObject(asset.RuntimeKey.ToString());
        }

        public async Task<PooledObjectOperation<GameObject>> TryGetGameObject(string key)
        {
            if (objectPools.ContainsKey(key) == false) { return null; }
            var pool = objectPools[key];

            while (pool.IsGenerating)
            {
                await Task.Delay(25);
            }

            if (pool.WaitingObjectsCount <= 0) // Allowing autoGrowth by default.
            {
                await pool.WarmupAsync(pool.Capacity + 1);
            }

            while(pool.WaitingObjectsCount <= 0)
            {
                await Task.Delay(25);
            }

            var obj = objectPools[key].Use();
            obj.Object.GetOrAddComponent<PooledObjectReference>().CurrentPooledOperation = obj;

            return obj;
        }

        public void TryReturnGameObject(PooledObjectOperation<GameObject> go)
        {
            if (go == null) { return; }
            go.Dispose();
        }
    }
}
#endif