using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ : MonoBehaviour
{
    public MiniGameBase miniGamePrefab;

    private MiniGameBase m_MiniGame;

    //TODO: Send arm in as parameter when starting mini game
    public void StartMiniGame()
    {
        if (!miniGamePrefab)
        {
            Debug.LogWarning("Trying to start mini-game that does not exits!", this);
            return;
        }

        m_MiniGame = Instantiate(miniGamePrefab);

        miniGamePrefab.OnStart();
    }
}
