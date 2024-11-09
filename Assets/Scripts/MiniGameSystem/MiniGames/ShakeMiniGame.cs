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
    [SerializeField] private RectTransform organTransform;
    [SerializeField] private Image progressbar;

    [Header("Input")]
    [SerializeField] private InputAction mouseDelta;

    private float m_Progress = 0;
    private Vector2 m_OrganStartPos;
    private PlayerHandController m_Hand;

    public override void OnStart(PlayerHandController controller)
    {
        m_Hand = controller;
        mouseDelta.Enable();
        mouseDelta.performed += OnMousePoition;
        m_OrganStartPos = organTransform.position;
    }

    private void OnDisable()
    {
        mouseDelta.performed -= OnMousePoition;
    }

    private void OnMousePoition(InputAction.CallbackContext obj)
    {
        Vector2 delta = obj.ReadValue<Vector2>();
        Vector2 toAdd = (Vector2)organTransform.position + delta.normalized * shakeStrenght;

        if(CheckIfInsideCircleArea(toAdd, out Vector2 add))
        { 
            organTransform.position = add;

            m_Progress += delta.magnitude * Time.deltaTime;
            //m_Hand.transform.position = (Vector2)m_Hand.transform.position + delta.normalized * shakeStrenght;

            progressbar.fillAmount = m_Progress / maxProgress;

            if(m_Progress >= maxProgress)
            {
                onCompletedSuccesfully?.Invoke();
            }
        }
    }

    public bool CheckIfInsideCircleArea(Vector2 p, out Vector2 add)
    {
        add = organTransform.position;
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
