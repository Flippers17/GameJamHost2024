using UnityEngine;

public class Organ : PickUpable
{
    public ToolType TriggerType => triggerType;

    [SerializeField] private MiniGame miniGamePrefab;
    [SerializeField] private ToolType triggerType;

    private MiniGame m_MiniGame;

    public void StartMiniGame(PlayerHandController controller)
    {
        if (!miniGamePrefab)
        {
            Debug.LogWarning("No miniGamePrefab set!", this);
            return;
        }

        m_MiniGame = Instantiate(miniGamePrefab);

        m_MiniGame.onCompletedSuccesfully += OnSucceesfullyCompleted;

        m_MiniGame.OnStart(controller);
    }

    private void OnSucceesfullyCompleted()
    {
        m_MiniGame.onCompletedSuccesfully -= OnSucceesfullyCompleted;
    }
}
