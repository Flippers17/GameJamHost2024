using UnityEngine;

public class Organ : PickUpable
{
    public ToolType TriggerType => toolType;
    public bool Finished => m_Finished;

    [SerializeField] private MiniGame miniGamePrefab;
    [SerializeField] private ToolType toolType;

    private bool m_Finished = false;

    private MiniGame m_MiniGame;
    private Transform m_Transform;

    private new void OnEnable()
    {
        base.OnEnable();
        m_Transform = transform;
    }

    public void StartMiniGame(PlayerHandController controller)
    {
        if (!miniGamePrefab)
        {
            Debug.LogWarning("No miniGamePrefab set!", this);
            return;
        }

        m_MiniGame = Instantiate(miniGamePrefab);
        MiniGameHandler.Instance.SetMiniGame(m_MiniGame);

        m_MiniGame.OnStart(controller, this);
    }

    public void MoveOrgan(Vector3 movement)
    {
        Vector3 newPos = m_Transform.position;
        newPos += new Vector3(movement.x, 0, movement.y) * Time.deltaTime;

        m_Transform.position = newPos;
    }

    public bool IsMiniGameActive => m_MiniGame;

    public void SetFinished() => m_Finished = true;
}
