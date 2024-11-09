using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShakeMiniGame : MiniGame
{
    [Header("Parameters")]
    [SerializeField] private float radius = 50;
    [SerializeField] private float shakeStrenght = 30;
    [SerializeField]
    private float _handMovementMultiplier = 1f;
    [SerializeField]
    private float _shakeRewardMultiplier = 10f;

    [Header("Objects")]
    [SerializeField] private Image progressbar;

    [Header("Input")]
    [SerializeField] private InputAction mouseDelta;

    private Vector2 m_OrganStartPos;
    private ParticleSystem m_BooldSplat;

    private Vector2 _lastDragDir;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        base.OnStart(controller, organ);

        mouseDelta.Enable();
        mouseDelta.performed += OnMousePoition;
        m_OrganStartPos = organ.transform.position;
        m_BooldSplat = organ.GetComponentInChildren<ParticleSystem>();
    }

    private void OnDisable()
    {
        mouseDelta.performed -= OnMousePoition;
    }

    private void OnMousePoition(InputAction.CallbackContext obj)
    {
        Vector2 movement = obj.ReadValue<Vector2>().normalized * shakeStrenght;
        if(Vector2.Dot(movement, _lastDragDir) < 0)
            movement *= _shakeRewardMultiplier;

        _lastDragDir = movement;
        Vector2 newPos = (Vector2)organ.transform.position + (movement * Time.deltaTime);

        

        if(CheckIfInsideCircleArea(newPos, out Vector2 add))
        {
            float progressProcentage = progress/neededProgress;

            IncreaseProgress(movement.magnitude * shakeStrenght * Time.deltaTime);
            playerHand.MoveHand(movement * _handMovementMultiplier * progressProcentage);
            organ.MoveOrgan(movement * _handMovementMultiplier * progressProcentage);
            m_BooldSplat.Play();

        }
    }

    public bool CheckIfInsideCircleArea(Vector2 p, out Vector2 add)
    {
        add = organ.transform.position;
        bool inside = false;

        if (!(Mathf.Abs(m_OrganStartPos.x - p.x) > radius))
        {
            add.x = p.x;
            inside = true;
        }
        if (!(Mathf.Abs(m_OrganStartPos.y - p.y) > radius))
        {
            add.y = p.y;
            inside = true;
        }

        return inside;
    }
}
