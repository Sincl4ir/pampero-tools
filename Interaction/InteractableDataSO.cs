using UnityEngine;

namespace Pampero.Tools.Interactors
{
    [CreateAssetMenu(fileName = "InteractableData", menuName = "Pampero/GOAP/Interactable Data")]
    public class InteractableDataSO : ScriptableObject
    {
        public IInteractable Interactable { get; protected set; }

        public void SetInteractable(IInteractable interactable)
        {
            Interactable = interactable;
        }

        public void UnsetInteractable()
        {
            Interactable = null;
        }
    }
}
//EOF.