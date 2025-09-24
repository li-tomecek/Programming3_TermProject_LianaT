using NUnit.Framework;
using UnityEngine;

public abstract class DropTarget : MonoBehaviour
{
    //Make sure the GameObject that applies this interface is on the "DragTarget" layer. This is just to help with raycasting for the target
    //Make sure the Droppable object implements the "IDroppable" interface
    [SerializeField] protected bool _isInteractable;

    public virtual void OnDragStartHover(IDroppable droppedObject) { }
    public virtual void OnDragEndHover() { }
    public virtual void OnDrop(IDroppable droppedObject) { }

    public void SetInteractable(bool interactable) { _isInteractable = interactable; }
    public bool IsInteractable() { return _isInteractable; }
}
