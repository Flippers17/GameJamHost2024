using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteadyHandMinigame : MiniGame
{
    [SerializeField]
    private InputAction _moveAction;

    private Vector2 startPos = Vector2.zero;

    [SerializeField]
    private float safeRadius = .5f;
    [SerializeField]
    private float _movementSpeed = .5f;
    [SerializeField]
    private float driftSpeed = .2f;

    [SerializeField]
    private float _loseProgressMultiplier = 2f;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        base.OnStart(controller, organ);

        _moveAction.Enable();
        _moveAction.performed += OnMoveInput;

        playerHand.MoveHand(new Vector2(organ.transform.position.x, organ.transform.position.z) - playerHand.GetHandPos());
        startPos = playerHand.GetHandPos();

    }

    private void Update()
    {
        Vector2 playerPos = playerHand.GetHandPos();
        Vector2 moveVector = (playerPos - startPos).normalized;

        playerHand.MoveHand(moveVector * driftSpeed);

        if((playerPos- startPos).sqrMagnitude < safeRadius * safeRadius)
        {
            IncreaseProgress(Time.deltaTime);
        }
        else
            IncreaseProgress(-_loseProgressMultiplier * Time.deltaTime);
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        playerHand.MoveHand(context.ReadValue<Vector2>().normalized * _movementSpeed);  
    }
}
