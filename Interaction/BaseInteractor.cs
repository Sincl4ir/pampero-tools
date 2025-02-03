using UnityEngine;

namespace Pampero.Tools.Interactors
{
    public abstract class BaseInteractor : MonoBehaviour, IInteractor
    {
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
            if (!CanInteractWith(interactable)) { return; }

            interactable.OnInteractionStart -= HandleInteractionStart;
            interactable.OnInteractionStart += HandleInteractionStart;
            interactable.OnInteractionComplete -= HandleSuccessfulInteraction;
            interactable.OnInteractionComplete += HandleSuccessfulInteraction;
            interactable.Interact();
        }

        protected abstract void HandleInteractionStart(InteractData data);
        protected abstract void HandleSuccessfulInteraction(InteractData data);

        public virtual void DisposeCurrentInteractable()
        {
            if (CurrentInteractable is null) { return; }

            CurrentInteractable.OnInteractionStart -= HandleInteractionStart;
            CurrentInteractable.OnInteractionComplete -= HandleSuccessfulInteraction;
            CurrentInteractable = null;
        }
    }
}
//EOF.