using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] InputAction clickAction;
    private Camera mainCamera;

    void OnEnable()
    {
        clickAction.Enable();
        clickAction.performed += OnClickPerformed;
        mainCamera = Camera.main;
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
        }
    }

}
