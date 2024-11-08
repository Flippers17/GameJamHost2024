using UnityEngine;
using UnityEngine.Events;

public class MiniGameHandler : MonoBehaviour
{
    public UnityEvent OnSuccesfulMiniGame;
    public UnityEvent OnUnsuccesfulMiniGame;

    private MiniGameBase m_CurrentMiniGame;

    public void SetMiniGame(MiniGameBase game) => m_CurrentMiniGame = game;
    public void SetMiniGameAndStart(MiniGameBase game)
    {
        m_CurrentMiniGame = game;
        StartMiniGame();
    }

    public void StartMiniGame()
    {
        if (!m_CurrentMiniGame)
        {
            Debug.LogWarning($"Trying to start a mini game that does not exits!", this);
            return;
        }
    }
}
