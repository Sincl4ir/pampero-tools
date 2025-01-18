#if ADDRESSABLE_ASSETS
using Addler.Runtime.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Pampero.Tools.Pooling.Addressables {
    public class AddressablePoolCreator : MonoBehaviour {
        [SerializeField] List<AddressableObjectPool.AddressableObjectSpawnProperty> _SpawnPropertyList = new List<AddressableObjectPool.AddressableObjectSpawnProperty>();

        [SerializeField] bool _DeletePoolsOnDestroy = false;

        void Awake()
        {
            foreach (var pool in _SpawnPropertyList)
            {
                try
                {
                    if (pool == null) continue;
                    var poolInstance = AddressableObjectPool.Instance.TryCreatePool(pool.Prefab);

                    if (poolInstance == null) continue;
                    poolInstance.BindTo(AddressableObjectPool.Instance.gameObject);

                    if (pool.PreWarmAmount <= 0) continue;

#if SKIP_AA
                    continue;
#endif
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    poolInstance.WarmupAsync(pool.PreWarmAmount);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        private void OnDestroy()
        {
            if (_DeletePoolsOnDestroy == false) return;

            foreach(var pool in _SpawnPropertyList)
            {
                if (pool == null) continue;
                AddressableObjectPool.Instance.TryRemovePool(pool.Prefab);
            }
        }
    }
}
#endif
