using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShakeMiniGame : MiniGame
{
    [Header("Parameters")]
    [SerializeField] private float radius = 50;
    [SerializeField] private float shakeStrenght = 30;
    [SerializeField] private float maxProgress = 1000;

    [Header("Objects")]
    [SerializeField] private Image progressbar;

    [Header("Input")]
    [SerializeField] private InputAction mouseDelta;

    private float m_Progress = 0;
    private Vector2 m_OrganStartPos;
    private ParticleSystem m_BooldSplat;

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
        Vector2 delta = obj.ReadValue<Vector2>();
        Vector2 newPos = (Vector2)organ.transform.position + delta.normalized * shakeStrenght;

        if(CheckIfInsideCircleArea(newPos, out Vector2 add))
        {
            float progressProcentage = m_Progress / maxProgress;

            m_Progress += newPos.magnitude * Time.deltaTime;
            playerHand.MoveHand(newPos.normalized * progressProcentage);
            organ.MoveOrgan(newPos.normalized * progressProcentage);
            m_BooldSplat.Play();

            progressbar.fillAmount = progressProcentage;

            if(m_Progress >= maxProgress)
            {
                Win();
            }
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
