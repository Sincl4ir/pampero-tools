using System;
using UnityEngine;
using UnityEngine.Events;

namespace Pampero.Tools.ProximityTrigger
{
    [RequireComponent(typeof(Collider))]
    public class ProximityTrigger : MonoBehaviour
    {
        [field: SerializeField] public LayerMask Layer { get; protected set; }
        [field: SerializeField] public Collider MyCollider { get; protected set; }

        public Transform OtherTransform;
        public bool IsTargetInside = false;

        [SerializeField] private UnityEvent<Collider> _onEnterAction;
        [SerializeField] private UnityEvent<Collider> _onExitAction;

        private void Awake()
        {
            if (MyCollider is null)
            {
                TryGetComponent<Collider>(out var collider);
                MyCollider = collider;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(Layer == (Layer | (1 << other.gameObject.layer)))) { return; }

            OtherTransform = other.transform;
            _onEnterAction?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!(Layer == (Layer | (1 << other.gameObject.layer)))) { return; }

            _onExitAction?.Invoke(other);
            OtherTransform = null;
            //isTargetInside = false;
        }

        public void SetProximityTriggers(Action<Collider> onEnter, Action<Collider> onExit)
        {
            if (onEnter != null)
            {
                _onEnterAction.AddListener((Collider other) => { onEnter(other); });
            }

            if (onExit != null)
            {
                _onExitAction.AddListener((Collider other) => { onExit(other); });
            }

            MyCollider.enabled = true;
        }

        public void RemoveProximityTriggers(Action<Collider> onEnter, Action<Collider> onExit)
        {
            if (onEnter != null)
            {
                _onEnterAction.RemoveListener((Collider other) => { onEnter(other); });
            }

            if (onExit != null)
            {
                _onExitAction.RemoveListener((Collider other) => { onExit(other); });
            }

            RefreshCollider();
        }

        public void ClearTriggers()
        {
            _onEnterAction.RemoveAllListeners();
            _onExitAction.RemoveAllListeners();
            MyCollider.enabled = false;
        }

        void RefreshCollider()
        {
            MyCollider.enabled = !(_onEnterAction.GetPersistentEventCount() == 0 && _onExitAction.GetPersistentEventCount() == 0);
        }
    }
}
//EOF.