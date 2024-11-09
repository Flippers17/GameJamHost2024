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
    bool min = false;

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

        if (ctx.ReadValue<Vector2>().y * mouseSpeed < 0 && !min)
            return;
        else if (ctx.ReadValue<Vector2>().y * mouseSpeed > 0 && min)
            return;

        m_CurrentZValue += ctx.ReadValue<Vector2>().y * mouseSpeed;
        
        if(m_CurrentZValue > maxZ && min)
        { 
            m_CurrentZValue = maxZ;
            min = !min;
        }
        else if (m_CurrentZValue < minZ && !min)
        {
            m_CurrentZValue = minZ;
            min = !min;
        }

        Debug.Log(Mathf.Abs(m_LastZValue - m_CurrentZValue) * Time.deltaTime);



        if(m_CurrentZValue < maxZ && m_CurrentZValue > minZ)
        {
            Debug.Log("Adding!");
            m_Progression += 0.5f * Time.deltaTime;
            progressBar.fillAmount = m_Progression;

            if(m_Progression >= 1)
            {
                //Win();
            }
            m_LastZValue = m_CurrentZValue;
        }

    }
}
