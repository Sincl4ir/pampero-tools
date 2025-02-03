using System;
using UnityEngine;

namespace Pampero.Tools.Interactors
{
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected InteractableDataSO _interactableDataSO;
        [SerializeField] protected InteractData _interactData;

        [SerializeField] protected bool _interactableLocked;

        public virtual bool CanBeInteracted => !_interactableLocked && !InteractionFulfilled;
        public virtual bool InteractionFulfilled { get; protected set; }

        public event Action<InteractData> OnInteractionStart;
        public event Action<InteractData> OnInteractionComplete;

        protected virtual void OnEnable()
        {
            _interactableDataSO.SetInteractable(this);
        }

        protected virtual void OnDisable()
        {
            _interactableDataSO.UnsetInteractable();
        }

        public void Interact() => HandleInteraction();
        protected abstract void HandleInteraction();
        protected virtual void TriggerInteractionStart() => OnInteractionStart?.Invoke(_interactData);
        protected virtual void TriggerInteractionComplete() => OnInteractionComplete?.Invoke(_interactData);
    }
}
//EOF.