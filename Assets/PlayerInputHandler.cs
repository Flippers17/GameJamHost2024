using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _input;

    [HideInInspector]
    public Vector2 movementVector;

    private void OnEnable()
    {
        _input.actions["Hand movement"].performed += OnHandMovement;
        _input.actions["Hand movement"].canceled += OnHandMovement;
    }

    private void OnDisable()
    {
        _input.actions["Hand movement"].performed -= OnHandMovement;
        _input.actions["Hand movement"].canceled -= OnHandMovement;
    }

    private void OnHandMovement(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
    }
}
