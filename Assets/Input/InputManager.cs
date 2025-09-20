using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions _actions;
    private Camera _mainCamera;

    //Mouse-Click Actions
    public InputAction ClickAction { get; private set; }

    void OnEnable()
    {
        _mainCamera = Camera.main;
        _actions = new InputSystem_Actions();

        ClickAction = _actions.Gameplay.Click;
        ClickAction.Enable();
        ClickAction.performed += OnClickPerformed;
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
        }
    }

}
