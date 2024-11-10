using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CutVeinsMiniGame : MiniGame
{
    [Header("Parameters")]
    [SerializeField] private float detectionRadius = 0.2f;
    [SerializeField] private Vector3 detectionArea;
    [SerializeField] private float mouseSpeed = 0.1f;

    [Header("Actions")]
    [SerializeField] private InputAction leftMouseButton;
    [SerializeField] private InputAction mouseDelta;

    [Header("Objects")]
    [SerializeField] private Image progressBar;

    private VeinMiniGame[] m_Veins;

    private int veinsClicked = 0;

    private bool m_IgnoreFirstClick = false;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        base.OnStart(controller, organ);

        mouseDelta.performed += OnMousePos;
        leftMouseButton.performed += OnMouseButtonClicked;
        mouseDelta.Enable();
        leftMouseButton.Enable();

        m_Veins = organ.GetComponentsInChildren<VeinMiniGame>();
        foreach(var vein in m_Veins)
        {
            vein.OnClicked += OnVeinClicked;
        }
    }

    private void OnDisable()
    {
        mouseDelta.performed -= OnMousePos;
        leftMouseButton.performed -= OnMouseButtonClicked;

        foreach (var vein in m_Veins)
        {
            vein.OnClicked -= OnVeinClicked;
        }
    }

    private void OnVeinClicked()
    {
        veinsClicked++;

        progressBar.fillAmount = (float)veinsClicked / m_Veins.Length;

        if(veinsClicked == m_Veins.Length)
        {
            Win();
        }
    }

    private void OnMousePos(InputAction.CallbackContext ctx)
    {
        playerHand.MoveHand(ctx.ReadValue<Vector2>() * mouseSpeed);
    }

    private void OnMouseButtonClicked(InputAction.CallbackContext ctx)
    {
        if (!m_IgnoreFirstClick)
        { 
            m_IgnoreFirstClick = true;
            return;
        }

        //Collider[] cols = Physics.OverlapSphere(playerHand.HandTransform.position, detectionRadius);
        Collider[] cols = Physics.OverlapBox(playerHand.HandTransform.position, detectionArea);

        foreach (Collider col in cols)
        {
            if(col.TryGetComponent(out VeinMiniGame vein) && !vein.Clicked)
            {
                vein.OnStart(null, null);
                return;
            }
        }
    }
}
