#if ADDRESSABLE_ASSETS
using Addler.Runtime.Core;
using Addler.Runtime.Foundation.EventDispatcher;
using Pampero.Tools.Singleton;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Pampero.Tools.Pooling.Addressables {
    public class AddressableParticlePool : Singleton<AddressableParticlePool> {

        public async Task<PooledObjectOperation<GameObject>> TryGetGameObject(AssetReference asset)
        {
            if (asset == null) return null;
            if (asset.RuntimeKeyIsValid() == false) return null;

            return await TryGetGameObject(asset.RuntimeKey.ToString());
        }

        public async Task<PooledObjectOperation<GameObject>> TryGetGameObject(string key)
        {
            var obj = await AddressableObjectPool.Instance.TryGetGameObject(key);

            if(obj == null) return null;

            await ParticleSystemFinishDispatcher(obj);
           
            return obj;
        }

        public async Task<PooledObjectOperation<GameObject>> ParticleSystemFinishDispatcher(PooledObjectOperation<GameObject> obj)
        {
            if (!obj.Object.TryGetComponent<ParticleSystemFinishedEventDispatcher>(out var dispatcher))
            {
                dispatcher = obj.Object.AddComponent<ParticleSystemFinishedEventDispatcher>();
            }

            obj.BindTo(dispatcher);
            return obj;
        }

        public void TryReturnGameObject(PooledObjectOperation<GameObject> Particle)
        {
            AddressableObjectPool.Instance.TryReturnGameObject(Particle);
        }
    }
}
#endif