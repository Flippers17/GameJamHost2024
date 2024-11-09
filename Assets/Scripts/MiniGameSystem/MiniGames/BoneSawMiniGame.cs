using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BoneSawMiniGame : MiniGame
{
    [SerializeField] private float mouseSpeed = 0.05f;
    [SerializeField] private float maxZ;
    [SerializeField] private float minZ;

    [Header("Actions")]
    [SerializeField] private InputAction mouseDelta;

    [Header("Objects")]
    [SerializeField] private Image progressBar;

    private float m_CurrentZValue;
    private float m_LastZValue;
    private float m_Progression;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        base.OnStart(controller, organ);

        mouseDelta.performed += OnMouseDelta;
        mouseDelta.Enable();
    }

    private void OnDisable()
    {
        mouseDelta.performed -= OnMouseDelta;
    }

    private void OnMouseDelta(InputAction.CallbackContext ctx)
    {
        m_CurrentZValue += ctx.ReadValue<Vector2>().y;

        if(m_CurrentZValue > maxZ)
            m_CurrentZValue = maxZ;
        else if (m_CurrentZValue < minZ)
            m_CurrentZValue = minZ;

        if(m_CurrentZValue < maxZ && m_CurrentZValue > minZ && Mathf.Abs(m_LastZValue - m_CurrentZValue) < 0.1f)
        {
            Debug.Log("Adding!");
            m_Progression += 0.1f * Time.deltaTime;
            progressBar.fillAmount = m_Progression;

            if(m_Progression >= 1)
            {
                Win();
            }
        }

        m_LastZValue = m_CurrentZValue;
    }
}
