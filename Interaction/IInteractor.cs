namespace Pampero.Tools.Interactors
{
    public interface IInteractor
    {
        IInteractable CurrentInteractable { get; }
        void Interact(IInteractable interactable);
        bool TryInteractWithCurrentInteractable();
        void DisposeCurrentInteractable();
    }
}
//EOF.