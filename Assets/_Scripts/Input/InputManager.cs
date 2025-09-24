using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputSystem_Actions _actions;
    private Camera _mainCamera;

    //Mouse-Click Actions
    public InputAction ClickAction { get; private set; }

    void OnEnable()
    {
        _mainCamera = Camera.main;
        _actions = new InputSystem_Actions();
        _actions.Gameplay.Enable();

        ClickAction = _actions.Gameplay.Click;
        ClickAction.performed += OnClickPerformed;
    }

    void OnDisable()
    {
        _actions.Gameplay.Disable();
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
            hit.collider.gameObject.GetComponent<IClickable>()?.OnClick();
    }
}
