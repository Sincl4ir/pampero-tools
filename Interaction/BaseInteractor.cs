using UnityEngine;
using UnityEngine.Events;

namespace Pampero.Tools.Interactors
{
    public abstract class BaseInteractor : MonoBehaviour, IInteractor
    {
        [field:SerializeField] public bool Active { get; protected set; } = true;

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

        public IInteractable CurrentInteractable { get; protected set; }

        protected virtual bool CanInteractWith(IInteractable interactable) => interactable.CanBeInteracted;
        
        public virtual void TryGetInteractable(Collider collider)
        {
            if (!collider.transform.TryGetComponent<IInteractable>(out var interactable)) { return; }
            CurrentInteractable = interactable;
        }

        public virtual void TryUnsetInteractable(Collider collider)
        {
            if (!collider.transform.TryGetComponent<IInteractable>(out var interactable)) { return; }
            if (CurrentInteractable != interactable) { return; }
            CurrentInteractable = null;
        }

        public virtual bool TryInteractWithCurrentInteractable()
        {
            if (CurrentInteractable is null) { return false; }
            Interact(CurrentInteractable);
            return true;
        }

        public virtual void Interact(IInteractable interactable)
        {
            if (interactable is null) { return; }
            if (!CanInteractWith(interactable)) { return; }

            interactable.InteractionStartedEvent -= HandleInteractionStart;
            interactable.InteractionStartedEvent += HandleInteractionStart;
            interactable.InteractionCompletedEvent -= HandleSuccessfulInteraction;
            interactable.InteractionCompletedEvent += HandleSuccessfulInteraction;
            //interactable.InteractionDisposedEvent -= HandleInteractionDisposal;
            //interactable.InteractionDisposedEvent += HandleInteractionDisposal;

            interactable.Interact();
        }

        //Made this one to be able to call it from a Unity Event
        public virtual void Interact() => Interact(CurrentInteractable);

        public virtual void DisposeCurrentInteractable()
        {
            if (CurrentInteractable is null) { return; }

            CurrentInteractable.InteractionStartedEvent -= HandleInteractionStart;
            CurrentInteractable.InteractionCompletedEvent -= HandleSuccessfulInteraction;
            //CurrentInteractable.InteractionDisposedEvent -= HandleInteractionDisposal;
            CurrentInteractable = null;
        }

        public void ToggleActive(bool enable) => Active = enable;

        protected virtual void HandleInteractionStart(InteractData data)
        {
            _onInteractionStarted?.Invoke(data);
        }
        protected virtual void HandleSuccessfulInteraction(InteractData data)
        {
            _onInteractionCompleted?.Invoke(data);
        }
        //protected abstract void HandleInteractionDisposal(InteractData data);
    }
}
//EOF.