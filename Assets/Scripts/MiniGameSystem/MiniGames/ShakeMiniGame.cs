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

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        base.OnStart(controller, organ);

        mouseDelta.Enable();
        mouseDelta.performed += OnMousePoition;
        m_OrganStartPos = organ.transform.position;
    }

    private void OnDisable()
    {
        mouseDelta.performed -= OnMousePoition;
    }

    private void OnMousePoition(InputAction.CallbackContext obj)
    {
        Vector2 delta = obj.ReadValue<Vector2>();
        Vector2 toAdd = (Vector2)organ.transform.position + delta.normalized * shakeStrenght;

        if(CheckIfInsideCircleArea(toAdd, out Vector2 add))
        {
            //organ.transform.position = add;

            m_Progress += delta.magnitude * Time.deltaTime;
            playerHand.MoveHand(delta.normalized);
            organ.MoveOrgan(delta.normalized * m_Progress / maxProgress);

            progressbar.fillAmount = m_Progress / maxProgress;

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

        if (!(Mathf.Abs(p.x - m_OrganStartPos.x) > radius))
        {
            add.x = p.x;
            inside = true;
        }
        if (!(Mathf.Abs(p.y - m_OrganStartPos.y) > radius))
        {
            add.y = p.y;
            inside = true;
        }

        return inside;
    }
}
