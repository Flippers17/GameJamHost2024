using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private bool _lockMouse = true;

    [SerializeField]
    private PlayerInput _input;

    [HideInInspector]
    public Vector2 movementVector;

    public UnityAction OnPickUp;
    public UnityAction OnLookUp;

    private void OnEnable()
    {
        _input.actions["Hand movement"].performed += OnHandMovement;
        _input.actions["Hand movement"].canceled += OnHandMovement;

        _input.actions["Pick Up"].performed += OnPickUpInput;
        _input.actions["Look Up"].performed += OnLookUpInput;

        if(_lockMouse)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        _input.actions["Hand movement"].performed -= OnHandMovement;
        _input.actions["Hand movement"].canceled -= OnHandMovement;

        _input.actions["Pick Up"].performed -= OnPickUpInput;
        _input.actions["Look Up"].performed -= OnLookUpInput;
    }

    private void OnHandMovement(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
    }
    
    private void OnPickUpInput(InputAction.CallbackContext context)
    {
        OnPickUp?.Invoke();
    }
    
    private void OnLookUpInput(InputAction.CallbackContext context)
    {
        OnLookUp?.Invoke();
    }
}
