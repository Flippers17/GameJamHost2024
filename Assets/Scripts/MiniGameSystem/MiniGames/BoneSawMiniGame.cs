using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BoneSawMiniGame : MiniGame
{
    [SerializeField] private float mouseSpeed = 0.05f;

    [Header("Actions")]
    [SerializeField] private InputAction mouseDelta;

    [Header("Objects")]
    [SerializeField] private Image progressBar;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        base.OnStart(controller, organ);

        mouseDelta.performed += OnMouseDelta;
    }

    private void OnDisable()
    {
        mouseDelta.performed -= OnMouseDelta;
    }

    private void OnMouseDelta(InputAction.CallbackContext ctx)
    {

    }
}
