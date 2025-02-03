using System;

namespace Pampero.Tools.Interactors
{
    public interface IInteractable
    {
        bool CanBeInteracted { get; }
        bool InteractionFulfilled { get; }
        event Action<InteractData> OnInteractionStart;
        event Action<InteractData> OnInteractionComplete;
        void Interact();
    }
}
//EOF.