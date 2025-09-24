using UnityEngine;

public interface IDropTarget
{
    //Make sure the GameObject that applies this interface is on the "DragTarget" layer. This is just to help with raycasting for the target
    //Make sure the Droppable object implements the "IDroppable" interface

    public void OnDragStartHover();
    public void OnDragEndHover();

    public void OnDrop(IDroppable droppedObject);
}
