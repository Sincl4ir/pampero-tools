using System;
using UnityEngine;
using UnityEngine.Events;

namespace Pampero.Tools.Interactors
{
    public interface IInteractable
    {
        Collider Collider { get; }
        bool CanBeInteracted { get; }
        bool InteractionFulfilled { get; }
        event UnityAction<InteractData> InteractionStartedEvent;
        event UnityAction<InteractData> InteractionCompletedEvent;
        event UnityAction<InteractData> InteractionDisposedEvent;
        void Interact();
        void DisposeInteraction();
    }
}
//EOF.