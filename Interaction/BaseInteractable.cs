using System;
using UnityEngine;
using UnityEngine.Events;

namespace Pampero.Tools.Interactors
{
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Collider _interactableCollider;
        [SerializeField] protected InteractableDataSO _interactableDataSO;
        [SerializeField] protected InteractData _interactData;
        
        protected bool _interactableLocked;

        public virtual bool CanBeInteracted => !_interactableLocked && !InteractionFulfilled;
        public virtual bool InteractionFulfilled { get; protected set; }
        public Collider Collider => _interactableCollider;

        [Header("Events")]
        [SerializeField] private UnityEvent<InteractData> _onInteractionStarted;
        [SerializeField] private UnityEvent<InteractData> _onInteractionCompleted;
        [SerializeField] private UnityEvent<InteractData> _onInteractionDisposed;

        public event UnityAction<InteractData> InteractionStartedEvent
        {
            add => _onInteractionStarted.AddListener(value);
            remove => _onInteractionStarted.RemoveListener(value);
        }

        public event UnityAction<InteractData> InteractionCompletedEvent
        {
            add => _onInteractionCompleted.AddListener(value);
            remove => _onInteractionCompleted.RemoveListener(value);
        }

        public event UnityAction<InteractData> InteractionDisposedEvent
        {
            add => _onInteractionDisposed.AddListener(value);
            remove => _onInteractionDisposed.RemoveListener(value);
        }

        protected virtual void OnEnable()
        {
            if (_interactableDataSO is null) { return; }
            _interactableDataSO.SetInteractable(this);
        }

        protected virtual void OnDisable()
        {
            if (_interactableDataSO is null) { return; }
            _interactableDataSO.UnsetInteractable();
        }

        public abstract void Interact();
        public abstract void HandleInteractionComplete();
        public abstract void DisposeInteraction();

        protected virtual void TriggerInteractionStart() => _onInteractionStarted?.Invoke(_interactData);
        protected virtual void TriggerInteractionComplete() => _onInteractionCompleted?.Invoke(_interactData);
        protected virtual void TriggerInteractionDisposal() => _onInteractionDisposed?.Invoke(_interactData);   
    }
}
//EOF.