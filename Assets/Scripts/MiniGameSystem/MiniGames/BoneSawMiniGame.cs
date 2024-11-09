using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BoneSawMiniGame : MiniGame
{
    [SerializeField] private float handMoveSpeedModifier = 1f;
    [SerializeField] private float progressionModifier = 1f;
    [SerializeField]
    private float _maxMovement = 3f;
    private float _remainingMovement = 0;

    private Vector2 currentSawDirection;

    [Header("Actions")]
    [SerializeField] private InputAction mouseDelta;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        base.OnStart(controller, organ);

        currentSawDirection = new Vector2(0, 1);
        _remainingMovement = _maxMovement / 2;
        mouseDelta.performed += OnMouseDelta;
        mouseDelta.Enable();
    }

    private void OnDisable()
    {
        mouseDelta.performed -= OnMouseDelta;
    }

    private void OnMouseDelta(InputAction.CallbackContext ctx)
    {
        float progressionMade = Vector2.Dot(ctx.ReadValue<Vector2>(), currentSawDirection.normalized) * Time.deltaTime * progressionModifier;

        if(progressionMade < 0)
        {
            currentSawDirection = -currentSawDirection;
            progressionMade = -progressionMade;
            _remainingMovement = _maxMovement - _remainingMovement;
        }
        else
        {
            progressionMade = Mathf.Min(_remainingMovement, progressionMade);
            _remainingMovement -= progressionMade;
        }

        playerHand.MoveHand(currentSawDirection.normalized * (progressionMade * handMoveSpeedModifier));
        IncreaseProgress(progressionMade);

    }
}
