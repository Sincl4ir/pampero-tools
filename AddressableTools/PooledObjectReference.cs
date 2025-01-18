using Addler.Runtime.Core;
using UnityEngine;

namespace Pampero.Tools.Pooling.Addressables 
{
    public class PooledObjectReference : MonoBehaviour {    //TODO Create a template for this. For now we are using only GOs
        [SerializeField] public PooledObjectOperation<GameObject> CurrentPooledOperation;

        public void Pool()
        {
            if (CurrentPooledOperation == null) return;

            AddressableObjectPool.Instance.TryReturnGameObject(CurrentPooledOperation);
            CurrentPooledOperation = null;
        }
    }
}
//EOF.