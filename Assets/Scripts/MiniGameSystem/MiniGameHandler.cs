using UnityEngine;
using UnityEngine.Events;

public class MiniGameHandler : MonoBehaviour
{
    public static MiniGameHandler Instance;

    private MiniGame m_CurrentMiniGame;

    private void OnEnable()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Too many " + typeof(MiniGameHandler).Name + " in scene!", this);
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        UnsubscribeMiniGame();
    }

    public void SetMiniGame(MiniGame game)
    {
        if (m_CurrentMiniGame)
            UnsubscribeMiniGame();

        m_CurrentMiniGame = game;
        m_CurrentMiniGame.onWin += OnWinMiniGame;
        m_CurrentMiniGame.onLost += OnLostMiniGame;
    }

    private void UnsubscribeMiniGame()
    {
        m_CurrentMiniGame.onWin -= OnWinMiniGame;
        m_CurrentMiniGame.onLost -= OnLostMiniGame;
    }

    private void OnWinMiniGame()
    {
        
    }
    private void OnLostMiniGame()
    {

    }
}
