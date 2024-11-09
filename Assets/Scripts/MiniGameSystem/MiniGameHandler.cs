using UnityEngine;
using UnityEngine.Events;

public class MiniGameHandler : MonoBehaviour
{
    public bool InMiniGame => m_CurrentMiniGame;

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
        if (!m_CurrentMiniGame)
            return;

        m_CurrentMiniGame.onWin -= OnWinMiniGame;
        m_CurrentMiniGame.onLost -= OnLostMiniGame;
    }

    private void OnWinMiniGame()
    {
        m_CurrentMiniGame = null;
    }
    private void OnLostMiniGame()
    {
        m_CurrentMiniGame = null;
    }

    public void QuitMiniGame() => m_CurrentMiniGame.Quit();
}
